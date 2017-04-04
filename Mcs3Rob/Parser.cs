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
                        Variables = ReadRobVariables(astFile),
                        CharacteristicMaps = ReadCharacteristicMaps(astFile),
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
            var variables = astConstants.Variables;

            var robConstants = new RobConstants()
            {
                Header = new RobConstantsHeader()
                {
                    Reserved0 = astConstants.Headers.ReadAsInt(0),
                    RamBlockAddressOffset = astConstants.Headers.ReadAsInt(1),
                },
                Parameters = ReadRobParameters(variables),
            };

            return robConstants;
        }

        private static RobVariables ReadRobVariables(AstFile astFile)
        {
            var astConstants = astFile.ReadAsAstDescriptionBlock("PROVARI2");
            var variables = astConstants.Variables;

            var robVariables = new RobVariables()
            {
                Header = new RobVariablesHeader()
                {
                    Reserved0 = astConstants.Headers.ReadAsInt(0),
                    RamBlockAddressOffset = astConstants.Headers.ReadAsInt(1),
                },
                Parameters = ReadRobParameters(variables),
            };

            return robVariables;
        }

        private static List<RobParameter> ReadRobParameters(AstSeq variables)
        {
            var parameters = variables.Items.Cast<AstSeq>().Select(variable =>
            {
                var item1 = variable.Items[1];
                var item1KnownHexValue = item1.IsKnownHexValue();

                var robParameter = new RobParameter
                {
                    ElementType = (RobElementType) variable.ReadAsUInt(0),
                    Bitmask = item1KnownHexValue ? new uint?(item1.ReadAsUInt()) : null,
                    Direction = item1KnownHexValue ? null : (RobDirection?) item1.ReadAsInt(),
                    MinimumLimit = variable.ReadAsLong(2),
                    MaximumLimit = variable.ReadAsLong(3),
                    Address = variable.ReadAsUInt(4),
                    DisplayMode = (RobDisplayMode) variable.ReadAsInt(5),
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
            return parameters;
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
            else if (controlUnitType.HasFlag(RobControlUnitType.ABUS))
            {
                throw new ParserContextErrorException(
                    "DEVPARAM not implemented for ABUS, need an example file to implement more.", astDeviceParams);
            }
            else
            {
                // ROM
                robDeviceParams = new RobRomDeviceParams()
                {
                    OffsetRomImage = headers.ReadAsSeq(0).ReadAsInt(0),
                    OffsetProvariAddress = headers.ReadAsSeq(0).ReadAsInt(1),
                };
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

        private static List<RobCharacteristicMap> ReadCharacteristicMaps(AstFile astFile)
        {
            var results = new List<RobCharacteristicMap>();

            var astCharline =
                astFile.DescriptionBlocks.Items.OfType<AstCharacteristicMapBlock>()
                    .Where(x => x.GroupName.Equals("CHARLINE2", StringComparison.OrdinalIgnoreCase));

            foreach (var descriptionBlock in astCharline)
            {
                var header = new RobCharacteristicMapHeader()
                {
                    Description = descriptionBlock.Headers.ReadAsText(0),
                    Label = descriptionBlock.Headers.ReadAsText(1),
                    AddressOfMap = descriptionBlock.Headers.ReadAsUInt(2),
                    Reserve3 = descriptionBlock.Headers.ReadAsInt(3),
                    Reserve4 = descriptionBlock.Headers.ReadAsInt(4),
                };

                var independantAxis = ReadRobIndependantAxis(descriptionBlock.IndependantAxis).ToList();
                var dependantAxis = ReadRobDependantAxis(descriptionBlock.DependantAxis);

                RobCharacteristicMap robCharacteristicMap;
                switch (descriptionBlock.GroupName.ToUpperInvariant())
                {
                    case "CHARLINE2":
                        robCharacteristicMap = new RobCharLine2()
                        {
                            Header = header,
                            UAxis = independantAxis[0],
                            QAxis = dependantAxis,
                        };
                        break;
                    case "CHARMAP2":
                        robCharacteristicMap = new RobCharMap2()
                        {
                            Header = header,
                            UAxis = independantAxis[0],
                            VAxis = independantAxis[1],
                            QAxis = dependantAxis,
                        };
                        break;
                    case "CHARSPACE":
                        robCharacteristicMap = new RobCharSpace()
                        {
                            Header = header,
                            UAxis = independantAxis[0],
                            VAxis = independantAxis[1],
                            WAxis = independantAxis[2],
                            QAxis = dependantAxis,
                        };
                        break;
                    default:
                        throw new ParserContextErrorException($"Unknown characteristic map type {descriptionBlock.GroupName}", descriptionBlock);
                }

                results.Add(robCharacteristicMap);
            }

            return results;
        }

        private static RobDependantAxis ReadRobDependantAxis(AstSeq dependantAxis)
        {
            return new RobDependantAxis()
            {
                ElementType = (RobElementType) dependantAxis.ReadAsInt(0),
                ValueDirection = (RobDirection) dependantAxis.ReadAsInt(1),
                MinimumLimit = dependantAxis.ReadAsLong(2),
                MaximumLimit = dependantAxis.ReadAsLong(3),
                Reserve4 = dependantAxis.ReadAsInt(4),
                Reserve5 = dependantAxis.ReadAsInt(5),
                Description = dependantAxis.ReadAsText(6),
                PhysicalLabelAbbreviation = dependantAxis.ReadAsText(7),
                EngineeringUnit = dependantAxis.ReadAsText(8),
                ConversionEquation = dependantAxis.ReadAsText(9),
                DecimalPlaces = dependantAxis.ReadAsInt(10),
                Reserve11 = dependantAxis.ReadAsInt(11),
                Reserve12 = dependantAxis.ReadAsInt(12),
                Reserve13 = dependantAxis.ReadAsInt(13),
            };
        }

        private static IEnumerable<RobIndependantAxis> ReadRobIndependantAxis(IEnumerable<AstIndependantAxis> descriptionBlockIndependantAxis)
        {
            foreach (var astIndependantAxis in descriptionBlockIndependantAxis)
            {
                var axisHeader = astIndependantAxis.AxisHeader;
                yield return new RobIndependantAxis()
                {
                    ElementType = (RobElementType) axisHeader.ReadAsInt(0),
                    ValueDirection = (RobDirection) axisHeader.ReadAsInt(1),
                    MinimumLimit = axisHeader.ReadAsLong(2),
                    MaximumLimit = axisHeader.ReadAsLong(3),
                    AddressGraduationTable = axisHeader.ReadAsInt(4),
                    AxisDirection = (RobDirection) axisHeader.ReadAsInt(5),
                    Description = axisHeader.ReadAsText(6),
                    PhysicalLabelAbbreviation = axisHeader.ReadAsText(7),
                    EngineeringUnit = axisHeader.ReadAsText(8),
                    ConversionEquation = axisHeader.ReadAsText(9),
                    DecimalPlaces = axisHeader.ReadAsInt(10),
                    Dimension = axisHeader.ReadAsInt(11),
                    GraduationTableAxisDirection = (RobDirection) axisHeader.ReadAsInt(12),
                    Reserve13 = axisHeader.ReadAsInt(13),
                    GraduationModel = axisHeader.ReadAsInt(14),
                    DefinitionValues =
                        astIndependantAxis.GraduationItems?.Items.Cast<AstFloat>().Select(x => x.Value).ToList(),
                };
            }
        }
    }
}