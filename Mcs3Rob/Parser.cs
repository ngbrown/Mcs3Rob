using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mcs3Rob
{
    public class Parser
    {
        public IList<string> ScanTokens(string path)
        {
            var scannedTokens = new List<string>();
            using (var file = File.OpenRead(path))
            {
                var scanner = new Mcs3RobScanner(file);
                scanner.Initialize();
                scanner.Error += (sender, args) => Error?.Invoke(sender, args);
                int tok;
                do
                {
                    tok = scanner.yylex();
                    scannedTokens.Add(((Token)tok).ToString());
                } while (tok > (int)Token.EOF);
            }

            return scannedTokens;
        }

        public string ParseAst(string path)
        {
            using (var file = File.OpenRead(path))
            {
                var parser = new Mcs3RobParser();
                parser.Error += (sender, args) => Error?.Invoke(sender, args);
                parser.Parse(file);

                return parser.AstFile?.ToString();
            }
        }

        public RobFile Read(string path)
        {
            using (var file = File.OpenRead(path))
            {
                var parser = new Mcs3RobParser();
                parser.Error += (sender, args) => Error?.Invoke(sender, args);
                parser.Parse(file);

                if (parser.AstFile == null)
                {
                    return null;
                }

                try
                {
                    var astFile = parser.AstFile;
                    var robFileHeader = ReadRobFileHeader(astFile.FileHeader);

                    return new RobFile()
                    {
                        FileHeader = robFileHeader,
                        DeviceParams = ReadRobDeviceParams(astFile, robFileHeader.ControlUnitType),
                        Constants = ReadRobConstants(astFile),
                    };

                }
                catch (Exception e)
                {
                    this.OnError(new ErrorEventArgs(new ErrorContext(5, e)));
                    return null;
                }
            }
        }

        public virtual event EventHandler<ErrorEventArgs> Error;

        protected virtual void OnError(ErrorEventArgs e)
        {
            Error?.Invoke(this, e);
        }

        private static RobConstants ReadRobConstants(AstFile astFile)
        {
            var astConstants = astFile.ReadAsAstDescriptionBlock("PROCONST2");
            var parameters =
                astConstants.Variables.Items.Cast<AstSeq>().Select(variable =>
                {
                    var item1 = variable.Items[1];
                    var item1KnownHexValue = item1.IsKnownHexValue();

                    var robParameter = new RobParameter
                    {
                        ElementType = (RobElementType) variable.ReadAsUInt(0),
                        Bitmask = item1KnownHexValue ? new uint?(item1.ReadAsUInt()) : null,
                        Direction = item1KnownHexValue ? null : (RobDirection?)item1.ReadAsInt(),
                        MinimumLimit = variable.ReadAsLong(2),
                        MaximumLimit = variable.ReadAsLong(3),
                        Address = variable.ReadAsUInt(4),
                        DisplayMode = (RobDisplayMode)variable.ReadAsInt(5),
                        Description = variable.ReadAsText(6),
                        VariableName = variable.ReadAsText(7),
                    };

                    if (variable.Items.Count > 8)
                    {
                        robParameter.EngineeringUnit = variable.ReadAsText(8);
                    }

                    if (variable.Items.Count > 9)
                    {
                        robParameter.ConversionEquation = variable.ReadAsText(9);
                    }

                    if (variable.Items.Count > 10)
                    {
                        robParameter.DecimalPlaces = variable.ReadAsInt(10);
                    }

                    return robParameter;
                }).ToList();

            var robConstants = new RobConstants()
            {
                Header = new RobConstantsHeader()
                {
                    Reserved0 = astConstants.Headers.ReadAsInt(0),
                    RamBlockAddressOffset = astConstants.Headers.ReadAsInt(1),
                },
                Parameters = parameters,
            };

            return robConstants;
        }

        private static RobDeviceParams ReadRobDeviceParams(AstFile astFile, RobControlUnitType controlUnitType)
        {
            var astDeviceParams = astFile.ReadAsAstDescriptionBlock("DEVPARAM");

            RobDeviceParams robDeviceParams;
            var headers = astDeviceParams.Headers;
            if (controlUnitType.HasFlag(RobControlUnitType.CAN))
            {
                // DEVPARAM layout seems to depend on the control unit type
                robDeviceParams = new RobCanDeviceParams()
                {
                    BaseAddressBinaryImage = headers.ReadAsUInt(0),
                    BaseAddressMeasurementData = headers.ReadAsUInt(1),
                    CanCcpIdentifierDto = headers.ReadAsInt(2),
                    CanCcpIdentifierCro = headers.ReadAsInt(3),
                    AnalogOutput1Control = headers.ReadAsInt(4),
                    AnalogOutput2Control = headers.ReadAsInt(5),
                    AnalogOutput3Control = headers.ReadAsInt(6),
                    AnalogOutput4Control = headers.ReadAsInt(7),
                };
            }
            else
            {
                throw new ParserContextErrorException(
                    "DEVPARAM not implemented for anything but CAN, need an example file to implement more.", astDeviceParams);
            }
            return robDeviceParams;
        }

        private static RobFileHeader ReadRobFileHeader(AstSeq astFileHeader)
        {
            var robControlUnitType = (RobControlUnitType) astFileHeader.ReadAsInt(2);
            RobFileHeader robFileHeader;

            if (robControlUnitType.HasFlag(RobControlUnitType.CAN))
            {
                robFileHeader = new RobFileHeaderCan()
                {
                    EmulationModuleNumber = astFileHeader.ReadAsInt(0),
                    RomSize = astFileHeader.ReadAsInt(1),
                    ControlUnitType = robControlUnitType,
                    CanBusFrequency = astFileHeader.ReadAsInt(3),
                    CanEcuNodeId = astFileHeader.ReadAsInt(4),
                    CanDefaultSamplingInterval = astFileHeader.ReadAsInt(5),
                    CanIdentifierDtoCcp = astFileHeader.ReadAsInt(6),
                    CanIdentifierCroCcp = astFileHeader.ReadAsInt(7),
                };
            }
            else if (robControlUnitType.HasFlag(RobControlUnitType.ABUS))
            {
                robFileHeader = new RobFileHeaderAnio()
                {
                    EmulationModuleNumber = astFileHeader.ReadAsInt(0),
                    RomSize = astFileHeader.ReadAsInt(1),
                    ControlUnitType = robControlUnitType,
                    AnioSignalAveragingTime = astFileHeader.ReadAsInt(3),
                    AnioReserved4 = astFileHeader.ReadAsInt(4),
                    AnioScanRate = astFileHeader.ReadAsInt(5),
                    AnioReserved6 = astFileHeader.ReadAsInt(6),
                    AnioReserved7 = astFileHeader.ReadAsInt(7),
                };
            }
            else
            {
                robFileHeader = new RobFileHeaderRom()
                {
                    EmulationModuleNumber = astFileHeader.ReadAsInt(0),
                    RomSize = astFileHeader.ReadAsInt(1),
                    ControlUnitType = robControlUnitType,
                    RomCycleTriggerAddress = astFileHeader.ReadAsInt(3),
                    RomReserved4 = astFileHeader.ReadAsInt(4),
                    RomBusMonitoringTimeGate = astFileHeader.ReadAsInt(5),
                    RomReserved6 = astFileHeader.ReadAsInt(6),
                    RomRamOffset = astFileHeader.ReadAsUInt(7),
                };
            }
            return robFileHeader;
        }
    }
}