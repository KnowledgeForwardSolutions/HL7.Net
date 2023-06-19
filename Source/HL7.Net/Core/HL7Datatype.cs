namespace HL7.Net.Core;

/// <summary>
///   Identifies the possible HL7 datatypes.
/// </summary>
public enum HL7Datatype
{
   ST_String,
   TX_Text,
   FT_FormattedText,
   NM_Numeric,
   DT_Date,
   TM_Time,
   TS_Timestamp,
   PN_PersonName,
   TN_TelephoneNumber,
   AD_Address,
   ID_CodedValue,
   SI_SequenceID,
   CM_Composite,
   CK_CompositeIDWithCheckDigit,
   CN_CompositeIDNumberAndName,
   CQ_CompositeQuantityWithUnits,
   CE_CodedElement,
   CF_CodedElementWithFormattedValues,
   RP_ReferencePointer,
   TQ_TimingQuantity,
   MO_Money,
}
