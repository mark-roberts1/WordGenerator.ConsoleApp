using System;
using System.Collections.Generic;
using System.Text;

namespace WordGenerator.ConsoleApp
{
    public static class WordGenerator
    {
        private static readonly char[] _consonants = new char[] { 'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Z' };
        private static readonly char[] _couldBeEither = new char[] { 'Y' };
        private static readonly char[] _vowels = new char[] { 'A', 'E', 'I', 'O', 'U' };
        private static CharType? _previous = null;
        private static int _occurrences = 0;

        public static string GetCapitalizedWord(int length)
        {
            _previous = null;
            _occurrences = 0;

            var word = new char[length];

            word[0] = GetAnyChar(false);

            for (int i = 1; i < length; i++)
            {
                word[i] = GetAnyChar(true);
            }

            return string.Concat(word);
        }

        public static string GetLowerCaseWord(int length)
        {
            _previous = null;
            _occurrences = 0;
            
            var word = new char[length];

            word[0] = GetAnyChar(true);

            for (int i = 1; i < length; i++)
            {
                word[i] = GetAnyChar(true);
            }

            return string.Concat(word);
        }

        private static char GetAnyChar(bool lowerCase)
        {
            var type = GetNextCharType(_previous);

            _occurrences = _previous == type ? _occurrences + 1 : 1;
            _previous = type;

            switch (type)
            {
                case CharType.Consonant:
                    return GetConsonant(lowerCase);
                case CharType.Vowel:
                    return GetVowel(lowerCase);
                case CharType.GreyArea:
                    return GetCouldBeEither(lowerCase);
            }

            throw new Exception();
        }

        private static char GetConsonant(bool lowerCase)
        {
            var maxConsonantIndex = _consonants.Length - 1;
            var randomChar = _consonants[GetRandomIndex(maxConsonantIndex)];

            if (lowerCase)
                return char.ToLower(randomChar);

            return randomChar;
        }

        private static char GetVowel(bool lowerCase)
        {
            var maxVowelIndex = _vowels.Length - 1;
            var randomChar = _vowels[GetRandomIndex(maxVowelIndex)];

            if (lowerCase)
                return char.ToLower(randomChar);

            return randomChar;
        }

        private static char GetCouldBeEither(bool lowerCase)
        {
            var maxIndex = _couldBeEither.Length - 1;

            var randomChar = _couldBeEither[GetRandomIndex(maxIndex)];

            if (lowerCase)
                return char.ToLower(randomChar);

            return randomChar;
        }

        private static int GetRandomIndex(int maxIndex)
        {
            if (maxIndex == 0) return 0;

            return new Random().Next(0, maxIndex);
        }

        private static CharType GetNextCharType(CharType? previousCharType)
        {
            if (ShouldUseWeirdOne()) return CharType.GreyArea;

            var nextType = (CharType)GetRandomIndex(2);
            return 
                nextType == previousCharType
                && _occurrences >= 2
                ? GetNextCharType(previousCharType) 
                : nextType;
        }

        private static bool ShouldUseWeirdOne()
        {
            var randomNum = GetRandomIndex(9);

            if (randomNum == 8) return true;

            return false;
        }

        private enum CharType
        {
            Consonant,
            Vowel,
            GreyArea
        }
    }
}
