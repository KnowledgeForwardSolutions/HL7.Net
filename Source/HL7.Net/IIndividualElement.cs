namespace HL7.Net;

/// <summary>
///   An element consisting of a single value.
/// </summary>
public interface IIndividualElement<T> : IElement
{
   /// <summary>
   ///   The value for this element.
   /// </summary>
   T Value { get; }
}
