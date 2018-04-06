#if NET

using System;
using Swashbuckle.Swagger;

#else

using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

#endif

namespace Swashbuckle.Examples.Auto
{
	public class SchemaSamplifier : ISchemaFilter
	{
		private readonly SampleFactory _factory;

		public SchemaSamplifier(SampleFactory factory)
		{
			_factory = factory;
		}

#if NET
		public void Apply(Schema schema, SchemaRegistry schemaRegistry, Type type)
		{
			object sample = _factory.BuildSample(type);
			if (sample != null)
			{
				schema.example = sample;
			}
		}
#else
		public void Apply(Schema model, SchemaFilterContext context)
		{
			object sample = _factory.BuildSample(context.SystemType);
			if (sample != null)
			{
				model.Example = sample;
			}
		}
#endif
	}
}