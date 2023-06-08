# S0002-ReadViaSpan

As a HL7.Net developer, I want to use the most efficient approach for processing
HL7 documents. Currently, this would mean Span<T> and Ranges

## Requirements

- GetLines method that accepts a ReadOnlySpan<Char> and returns IEnumerable<Range>
that can be used to create slices of the original span for each line in the 
document.

## Definition of DONE

1. GetLines method
2. Unit tests for GetLine method
3. Benchmarks for GetLine method
