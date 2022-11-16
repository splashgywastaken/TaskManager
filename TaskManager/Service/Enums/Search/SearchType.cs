namespace TaskManager.Service.Enums.Search;

public enum SearchType
{
    // Full word match
    FullMatch,
    // Partial word match
    PartialMatch,
    // A complete match of the word considering the case of characters
    LetterCaseFullMatch,
    // Partial match of a word considering the case of characters
    LetterCasePartialMatch
}