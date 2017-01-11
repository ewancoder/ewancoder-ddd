namespace Ewancoder.DDD.EFEventStore
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Type factory used to resolve type using unique type identifier.
    /// </summary>
    public sealed class TypeFactory : ITypeFactory
    {
        /// <summary>
        /// Used to get known type.
        /// </summary>
        private readonly IDictionary<string, Type> _knownTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeFactory"/> class.
        /// </summary>
        /// <param name="knownTypes">Known types dictionary containing type
        /// identifier as a key and concrete type as a value.</param>
        public TypeFactory(IDictionary<string, Type> knownTypes)
        {
            _knownTypes = knownTypes;
        }

        /// <summary>
        /// Resolves type using type identifier.
        /// </summary>
        /// <param name="typeIdentifier">Unique type identifier.</param>
        /// <returns>Resolved type.</returns>
        public Type Resolve(string typeIdentifier)
        {
            return _knownTypes[typeIdentifier];
        }
    }
}
