#if NET

using System;
using Swashbuckle.Swagger;

#else

using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

#endif

namespace Swashbuckle.Examples.Auto
{
	/// <summary>
	/// Enable generation of sample instances using a <see cref="SampleFactory"/> as an example of a model.
	/// </summary>
	public class SchemaSamplifier : ISchemaFilter
	{
		private readonly SampleFactory _factory;

		/// <summary>
		/// Creates an instance that uses the provided factory.
		/// </summary>
		/// <param name="factory">Factory instance.</param>
		public SchemaSamplifier(SampleFactory factory)
		{
			_factory = factory;
		}

#if NET
		/// <summary>
		/// Modifies the schema for a given model so that it includes an example element
		/// that contains controlled sample values.
		/// </summary>
		/// <remarks>If the type is not decorated with the <see cref="SamplifyAttribute"/>, the schema will not be modified.</remarks>
		/// <param name="schema">The model to modify.</param>
		/// <param name="schemaRegistry">The registry.</param>
		/// <param name="type">The corresponding type to build.</param>
		public void Apply(Schema schema, SchemaRegistry schemaRegistry, Type type)
		{
			object sample = _factory.BuildSample(type);
			if (sample != null)
			{
				schema.example = sample;
			}
		}
#else
		/// <summary>
		/// Modifies the schema for a given model so that it includes an example element
		/// that contains controlled sample values.
		/// </summary>
		/// <remarks>If the type is not decorated with the <see cref="SamplifyAttribute"/>, the schema will not be modified.</remarks>
		/// <param name="model">The model to modify.</param>
		/// <param name="context">The context that contains the corresponding type to build.</param>
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