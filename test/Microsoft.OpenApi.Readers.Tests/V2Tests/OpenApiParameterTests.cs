﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.IO;
using FluentAssertions;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Reader.ParseNodes;
using Microsoft.OpenApi.Reader.V2;
using Xunit;

namespace Microsoft.OpenApi.Readers.Tests.V2Tests
{
    [Collection("DefaultSettings")]
    public class OpenApiParameterTests
    {
        private const string SampleFolderPath = "V2Tests/Samples/OpenApiParameter/";

        [Fact]
        public void ParseBodyParameterShouldSucceed()
        {
            // Arrange
            MapNode node;
            using (var stream = Resources.GetStream(Path.Combine(SampleFolderPath, "bodyParameter.yaml")))
            {
                node = TestHelper.CreateYamlMapNode(stream);
            }

            // Act
            var parameter = OpenApiV2Deserializer.LoadParameter(node);

            // Assert
            // Body parameter is currently not translated via LoadParameter.
            // This design may be revisited and this unit test may likely change.
            parameter.Should().BeNull();
        }

        [Fact]
        public void ParsePathParameterShouldSucceed()
        {
            // Arrange
            MapNode node;
            using (var stream = Resources.GetStream(Path.Combine(SampleFolderPath, "pathParameter.yaml")))
            {
                node = TestHelper.CreateYamlMapNode(stream);
            }

            // Act
            var parameter = OpenApiV2Deserializer.LoadParameter(node);

            // Assert
            parameter.Should().BeEquivalentTo(
                new OpenApiParameter
                {
                    In = ParameterLocation.Path,
                    Name = "username",
                    Description = "username to fetch",
                    Required = true,
                    Schema = new()
                    {
                        Type = "string"
                    }
                });
        }

        [Fact]
        public void ParseQueryParameterShouldSucceed()
        {
            // Arrange
            MapNode node;
            using (var stream = Resources.GetStream(Path.Combine(SampleFolderPath, "queryParameter.yaml")))
            {
                node = TestHelper.CreateYamlMapNode(stream);
            }

            // Act
            var parameter = OpenApiV2Deserializer.LoadParameter(node);

            // Assert
            parameter.Should().BeEquivalentTo(
                new OpenApiParameter
                {
                    In = ParameterLocation.Query,
                    Name = "id",
                    Description = "ID of the object to fetch",
                    Required = false,
                    Schema = new()
                    {
                        Type = "array",
                        Items = new()
                        {
                            Type = "string"
                        }
                    },
                    Style = ParameterStyle.Form,
                    Explode = true
                });
        }

        [Fact]
        public void ParseParameterWithNullLocationShouldSucceed()
        {
            // Arrange
            MapNode node;
            using (var stream = Resources.GetStream(Path.Combine(SampleFolderPath, "parameterWithNullLocation.yaml")))
            {
                node = TestHelper.CreateYamlMapNode(stream);
            }

            // Act
            var parameter = OpenApiV2Deserializer.LoadParameter(node);

            // Assert
            parameter.Should().BeEquivalentTo(
                new OpenApiParameter
                {
                    In = null,
                    Name = "username",
                    Description = "username to fetch",
                    Required = true,
                    Schema = new()
                    {
                        Type = "string"
                    }
                });
        }

        [Fact]
        public void ParseParameterWithNoLocationShouldSucceed()
        {
            // Arrange
            MapNode node;
            using (var stream = Resources.GetStream(Path.Combine(SampleFolderPath, "parameterWithNoLocation.yaml")))
            {
                node = TestHelper.CreateYamlMapNode(stream);
            }

            // Act
            var parameter = OpenApiV2Deserializer.LoadParameter(node);

            // Assert
            parameter.Should().BeEquivalentTo(
                new OpenApiParameter
                {
                    In = null,
                    Name = "username",
                    Description = "username to fetch",
                    Required = true,
                    Schema = new()
                    {
                        Type = "string"
                    }
                });
        }

        [Fact]
        public void ParseParameterWithNoSchemaShouldSucceed()
        {
            // Arrange
            MapNode node;
            using (var stream = Resources.GetStream(Path.Combine(SampleFolderPath, "parameterWithNoSchema.yaml")))
            {
                node = TestHelper.CreateYamlMapNode(stream);
            }

            // Act
            var parameter = OpenApiV2Deserializer.LoadParameter(node);

            // Assert
            parameter.Should().BeEquivalentTo(
                new OpenApiParameter
                {
                    In = null,
                    Name = "username",
                    Description = "username to fetch",
                    Required = false
                });
        }

        [Fact]
        public void ParseParameterWithUnknownLocationShouldSucceed()
        {
            // Arrange
            MapNode node;
            using (var stream = Resources.GetStream(Path.Combine(SampleFolderPath, "parameterWithUnknownLocation.yaml")))
            {
                node = TestHelper.CreateYamlMapNode(stream);
            }

            // Act
            var parameter = OpenApiV2Deserializer.LoadParameter(node);

            // Assert
            parameter.Should().BeEquivalentTo(
                new OpenApiParameter
                {
                    In = null,
                    Name = "username",
                    Description = "username to fetch",
                    Required = true,
                    Schema = new()
                    {
                        Type = "string"
                    }
                });
        }

        [Fact]
        public void ParseParameterWithDefaultShouldSucceed()
        {
            // Arrange
            MapNode node;
            using (var stream = Resources.GetStream(Path.Combine(SampleFolderPath, "parameterWithDefault.yaml")))
            {
                node = TestHelper.CreateYamlMapNode(stream);
            }

            // Act
            var parameter = OpenApiV2Deserializer.LoadParameter(node);

            // Assert
            parameter.Should().BeEquivalentTo(
                new OpenApiParameter
                {
                    In = ParameterLocation.Path,
                    Name = "username",
                    Description = "username to fetch",
                    Required = true,
                    Schema = new()
                    {
                        Type = "number",
                        Format = "float",
                        Default = new OpenApiAny(5)
                    }
                }, options => options.IgnoringCyclicReferences());
        }

        [Fact]
        public void ParseParameterWithEnumShouldSucceed()
        {
            // Arrange
            MapNode node;
            using (var stream = Resources.GetStream(Path.Combine(SampleFolderPath, "parameterWithEnum.yaml")))
            {
                node = TestHelper.CreateYamlMapNode(stream);
            }

            // Act
            var parameter = OpenApiV2Deserializer.LoadParameter(node);

            // Assert
            parameter.Should().BeEquivalentTo(
                new OpenApiParameter
                {
                    In = ParameterLocation.Path,
                    Name = "username",
                    Description = "username to fetch",
                    Required = true,
                    Schema = new()
                    {
                        Type = "number",
                        Format = "float",
                        Enum =
                        {
                            new OpenApiAny(7).Node,
                            new OpenApiAny(8).Node,
                            new OpenApiAny(9).Node
                        }
                    }
                }, options => options.IgnoringCyclicReferences());
        }
    }
}
