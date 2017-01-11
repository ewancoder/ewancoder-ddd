namespace Ewancoder.DDD.EFEventStore
{
    using System;

    /// <summary>
    /// Type factory used to resolve type using unique type identifier.
    /// </summary>
    public interface ITypeFactory
    {
        /// <summary>
        /// Resolves type using type identifier.
        /// </summary>
        /// <param name="typeIdentifier">Unique type identifier.</param>
        /// <returns>Resolved type.</returns>
        Type Resolve(string typeIdentifier);
    }
}
