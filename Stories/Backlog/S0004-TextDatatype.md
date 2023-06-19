# S0004-TextDatatype

As a developer who uses HL7.Net, I want the HL7 TX datatype to be fully supported
when parsing messages.

## Requirements

- TextField class that implements TX datatype as per HL7 V2.2 spec section 2.8.10.2
- Support field max length
- Render escape sequences as per HL7 V2.2 spec section 2.4.6
- Repeat delimiter => carriage return in text

## Definition of DONE

1. TextField class
2. Unit tests for TextField method
