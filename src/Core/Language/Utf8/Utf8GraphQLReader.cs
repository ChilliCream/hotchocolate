using System.Buffers;
using System;
using System.Runtime.CompilerServices;
using System.Globalization;
using HotChocolate.Language.Properties;

namespace HotChocolate.Language
{
    public ref partial struct Utf8GraphQLReader
    {
        private static readonly byte _space = (byte)' ';
        private int _nextNewLines;
        private ReadOnlySpan<byte> _graphQLData;
        private ReadOnlySpan<byte> _value;
        private int _length;
        private int _position;
        private TokenKind _kind;
        public int _start;
        public int _end;
        public int _line;
        public int _lineStart;
        public int _column;

        public Utf8GraphQLReader(ReadOnlySpan<byte> graphQLData)
        {
            _kind = TokenKind.StartOfFile;
            _start = 0;
            _end = 0;
            _lineStart = 0;
            _line = 1;
            _column = 1;
            _graphQLData = graphQLData;
            _length = graphQLData.Length;
            _nextNewLines = 0;
            _position = 0;
            _value = null;
        }

        public ReadOnlySpan<byte> GraphQLData => _graphQLData;

        /// <summary>
        /// Gets the kind of <see cref="SyntaxToken" />.
        /// </summary>
        public TokenKind Kind => _kind;

        /// <summary>
        /// Gets the character offset at which this node begins.
        /// </summary>
        public int Start => _start;

        /// <summary>
        /// Gets the character offset at which this node ends.
        /// </summary>
        public int End => _end;

        /// <summary>
        /// The current position of the lexer pointer.
        /// </summary>
        public int Position => _position;

        /// <summary>
        /// Gets the 1-indexed line number on which this
        /// <see cref="SyntaxToken" /> appears.
        /// </summary>
        public int Line => _line;

        /// <summary>
        /// The source index of where the current line starts.
        /// </summary>
        public int LineStart => _lineStart;

        /// <summary>
        /// Gets the 1-indexed column number at which this
        /// <see cref="SyntaxToken" /> begins.
        /// </summary>
        public int Column => _column;

        /// <summary>
        /// For non-punctuation tokens, represents the interpreted
        /// value of the token.
        /// </summary>
        public ReadOnlySpan<byte> Value => _value;

        public bool Read()
        {
            if (_position == 0)
            {
                SkipBoml();
            }

            if (_position > 940)
            {

            }

            SkipWhitespaces();
            UpdateColumn();

            if (IsEndOfStream())
            {
                _start = _position;
                _end = _position;
                _kind = TokenKind.EndOfFile;
                _value = null;
                return false;
            }

            byte code = _graphQLData[_position];

            if (GraphQLConstants.IsLetterOrUnderscore(code))
            {
                ReadNameToken();
                return true;
            }

            if (GraphQLConstants.IsPunctuator(code))
            {
                ReadPunctuatorToken(code);
                return true;
            }

            if (GraphQLConstants.IsDigitOrMinus(code))
            {
                ReadNumberToken(code);
                return true;
            }

            if (code == GraphQLConstants.Hash)
            {
                ReadCommentToken();
                return true;
            }

            if (code == GraphQLConstants.Quote)
            {
                if (_length > _position + 2
                    && _graphQLData[_position + 1] == GraphQLConstants.Quote
                    && _graphQLData[_position + 2] == GraphQLConstants.Quote)
                {
                    _position += 2;
                    ReadBlockStringToken();
                }
                else
                {
                    ReadStringValueToken();
                }
                return true;
            }

            throw new SyntaxException(this,
                $"Unexpected character `{(char)code}` ({code}).");
        }

        /// <summary>
        /// Reads name tokens as specified in
        /// http://facebook.github.io/graphql/October2016/#Name
        /// [_A-Za-z][_0-9A-Za-z]
        /// from the current lexer state.
        /// </summary>
        /// <param name="state">The lexer state.</param>
        /// <param name="previous">The previous-token.</param>
        /// <returns>
        /// Returns the name token read from the current lexer state.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReadNameToken()
        {
            var start = _position;
            var position = _position;

            do
            {
                position++;
            }
            while (position < _length
                && GraphQLConstants.IsLetterOrDigitOrUnderscore(
                    _graphQLData[position]));

            _kind = TokenKind.Name;
            _start = start;
            _end = position;
            _value = _graphQLData.Slice(start, position - start);
            _position = position;
        }

        /// <summary>
        /// Reads punctuator tokens as specified in
        /// http://facebook.github.io/graphql/October2016/#sec-Punctuators
        /// one of ! $ ( ) ... : = @ [ ] { | }
        /// additionaly the reader will tokenize ampersands.
        /// </summary>
        /// <param name="state">
        /// The lexer state.
        /// </param>
        /// <param name="previous">
        /// The previous-token.
        /// </param>
        /// <param name="firstCode">
        /// The first character of the punctuator.
        /// </param>
        /// <returns>
        /// Returns the punctuator token read from the current lexer state.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReadPunctuatorToken(in byte code)
        {
            _start = _position;
            _end = ++_position;
            _value = null;

            switch (code)
            {
                case GraphQLConstants.Bang:
                    _kind = TokenKind.Bang;
                    break;

                case GraphQLConstants.Dollar:
                    _kind = TokenKind.Dollar;
                    break;

                case GraphQLConstants.Ampersand:
                    _kind = TokenKind.Ampersand;
                    break;

                case GraphQLConstants.LeftParenthesis:
                    _kind = TokenKind.LeftParenthesis;
                    break;

                case GraphQLConstants.RightParenthesis:
                    _kind = TokenKind.RightParenthesis;
                    break;

                case GraphQLConstants.Colon:
                    _kind = TokenKind.Colon;
                    break;

                case GraphQLConstants.Equal:
                    _kind = TokenKind.Equal;
                    break;

                case GraphQLConstants.At:
                    _kind = TokenKind.At;
                    break;

                case GraphQLConstants.LeftBracket:
                    _kind = TokenKind.LeftBracket;
                    break;

                case GraphQLConstants.RightBracket:
                    _kind = TokenKind.RightBracket;
                    break;

                case GraphQLConstants.LeftBrace:
                    _kind = TokenKind.LeftBrace;
                    break;

                case GraphQLConstants.RightBrace:
                    _kind = TokenKind.RightBrace;
                    break;

                case GraphQLConstants.Pipe:
                    _kind = TokenKind.Pipe;
                    break;

                case GraphQLConstants.Dot:
                    if (_graphQLData[_position] == GraphQLConstants.Dot
                        && _graphQLData[_position + 1] == GraphQLConstants.Dot)
                    {
                        _position += 2;
                        _end = _position;
                        _kind = TokenKind.Spread;
                    }
                    else
                    {
                        _position--;
                        throw new SyntaxException(this,
                            string.Format(CultureInfo.InvariantCulture,
                                LangResources.Reader_InvalidToken,
                                TokenKind.Spread));
                    }
                    break;

                default:
                    _position--;
                    throw new SyntaxException(this,
                        string.Format(CultureInfo.InvariantCulture,
                            LangResources.Reader_UnexpectedPunctuatorToken,
                            code));
            }
        }

        /// <summary>
        /// Reads int tokens as specified in
        /// http://facebook.github.io/graphql/October2016/#IntValue
        /// or a float tokens as specified in
        /// http://facebook.github.io/graphql/October2016/#FloatValue
        /// from the current lexer state.
        /// </summary>
        /// <param name="state">The lexer state.</param>
        /// <param name="previous">The previous-token.</param>
        /// <param name="firstCode">
        /// The first character of the int or float token.
        /// </param>
        /// <returns>
        /// Returns the int or float tokens read from the current lexer state.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReadNumberToken(byte firstCode)
        {
            int start = _position;
            byte code = firstCode;
            var isFloat = false;

            if (code == GraphQLConstants.Minus)
            {
                code = _graphQLData[++_position];
            }

            if (code == GraphQLConstants.Zero)
            {
                code = _graphQLData[++_position];
                if (GraphQLConstants.IsDigit(code))
                {
                    throw new SyntaxException(this,
                        "Invalid number, unexpected digit after 0: " +
                        $"`{(char)code}` ({code}).");
                }
            }
            else
            {
                ReadDigits(code);
                if (_position < _length)
                {
                    code = _graphQLData[_position];
                }
                else
                {
                    code = _space;
                }
            }

            if (code == GraphQLConstants.Dot)
            {
                isFloat = true;
                code = _graphQLData[++_position];
                ReadDigits(code);
                if (_position < _length)
                {
                    code = _graphQLData[_position];
                }
                else
                {
                    code = GraphQLConstants.Space;
                }
            }

            if ((code | (char)0x20) == GraphQLConstants.E)
            {
                isFloat = true;
                code = _graphQLData[++_position];
                if (code == GraphQLConstants.Plus
                    || code == GraphQLConstants.Minus)
                {
                    code = _graphQLData[++_position];
                }
                ReadDigits(code);
            }

            _kind = isFloat ? TokenKind.Float : TokenKind.Integer;
            _start = start;
            _end = _position;
            _value = _graphQLData.Slice(start, _position - start);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReadDigits(byte firstCode)
        {
            if (!firstCode.IsDigit())
            {
                throw new SyntaxException(this,
                    "Invalid number, expected digit but got: " +
                    $"`{(char)firstCode}` ({firstCode}).");
            }

            while (++_position < _length
                && GraphQLConstants.IsDigit(_graphQLData[_position]))
            { }
        }

        /// <summary>
        /// Reads comment tokens as specified in
        /// http://facebook.github.io/graphql/October2016/#sec-Comments
        /// #[\u0009\u0020-\uFFFF]*
        /// from the current lexer state.
        /// </summary>
        /// <param name="state">The lexer state.</param>
        /// <param name="previous">The previous-token.</param>
        /// <returns>
        /// Returns the comment token read from the current lexer state.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReadCommentToken()
        {
            var start = _position;
            var trimStart = _position + 1;
            bool trim = true;

            while (++_position < _length
                && !GraphQLConstants.IsControlCharacter(
                    _graphQLData[_position]))
            {
                if (trim)
                {
                    switch (_graphQLData[_position])
                    {
                        case GraphQLConstants.Hash:
                        case GraphQLConstants.Space:
                        case GraphQLConstants.Tab:
                            trimStart = _position;
                            break;

                        default:
                            trim = false;
                            break;
                    }
                }
            }

            _kind = TokenKind.Comment;
            _start = start;
            _end = _position;
            _value = _graphQLData.Slice(trimStart, _position - trimStart);
        }

        /// <summary>
        /// Reads string tokens as specified in
        /// http://facebook.github.io/graphql/October2016/#StringValue
        /// "([^"\\\u000A\u000D]|(\\(u[0-9a-fA-F]{4}|["\\/bfnrt])))*"
        /// from the current lexer state.
        /// </summary>
        /// <param name="state">The lexer state.</param>
        /// <param name="previous">The previous-token.</param>
        /// <returns>
        /// Returns the string value token read from the current lexer state.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReadStringValueToken()
        {
            var start = _position;
            byte code = _graphQLData[++_position];

            while (code != GraphQLConstants.NewLine
                && code != GraphQLConstants.Return)
            {
                // closing Quote (")
                if (code == GraphQLConstants.Quote)
                {
                    _kind = TokenKind.String;
                    _start = start;
                    _end = _position;
                    _value = _graphQLData.Slice(start + 1, _position - start - 1);
                    _position++;
                    return;
                }

                // SourceCharacter
                if (GraphQLConstants.IsControlCharacter(code))
                {
                    throw new SyntaxException(this,
                        $"Invalid character within String: {code}.");
                }

                if (code == GraphQLConstants.Backslash)
                {
                    code = _graphQLData[++_position];
                    if (!GraphQLConstants.IsValidEscapeCharacter(code))
                    {
                        throw new SyntaxException(this,
                            $"Invalid character escape sequence: \\{code}.");
                    }
                }

                code = _graphQLData[++_position];
            }

            throw new SyntaxException(this, "Unterminated string.");
        }

        /// <summary>
        /// Reads block string tokens as specified in
        /// http://facebook.github.io/graphql/draft/#BlockStringCharacter
        /// from the current lexer state.
        /// </summary>
        /// <param name="state">The lexer state.</param>
        /// <param name="previous">The previous-token.</param>
        /// <returns>
        /// Returns the block string token read from the current lexer state.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReadBlockStringToken()
        {
            var start = _position - 2;
            _nextNewLines = 0;

            byte code = _graphQLData[++_position];

            while (!IsEndOfStream())
            {
                // Closing Triple-Quote (""")
                if (code == GraphQLConstants.Quote
                    && _graphQLData[_position + 1] == GraphQLConstants.Quote
                    && _graphQLData[_position + 2] == GraphQLConstants.Quote)
                {
                    _kind = TokenKind.BlockString;
                    _start = start;
                    _end = _position + 2;
                    _value = _graphQLData.Slice(
                        start + 3,
                        _position - start - 3);
                    _position = _end + 1;
                    return;
                }

                // SourceCharacter
                if (code.IsControlCharacter()
                    && code != GraphQLConstants.NewLine
                    && code != GraphQLConstants.Return)
                {
                    throw new SyntaxException(this,
                        $"Invalid character within String: ${code}.");
                }

                if (code == GraphQLConstants.Backslash
                    && _graphQLData[_position + 1] == GraphQLConstants.Quote
                    && _graphQLData[_position + 2] == GraphQLConstants.Quote
                    && _graphQLData[_position + 3] == GraphQLConstants.Quote)
                {
                    _position += 3;
                }
                else if (code == GraphQLConstants.NewLine)
                {
                    int next = _position + 1;
                    if (next < _length
                        && _graphQLData[next] == GraphQLConstants.Return)
                    {
                        _position = next;
                    }
                    _nextNewLines++;
                }
                else if (code == GraphQLConstants.Return)
                {
                    int next = _position + 1;
                    if (next < _length
                        && _graphQLData[next] == GraphQLConstants.NewLine)
                    {
                        _position = next;
                    }
                    _nextNewLines++;
                }

                code = _graphQLData[++_position];
            }

            throw new SyntaxException(this, "Unterminated string.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SkipWhitespaces()
        {
            if (IsEndOfStream())
            {
                return;
            }

            if (_nextNewLines > 0)
            {
                NewLine(_nextNewLines);
                _nextNewLines = 0;
            }

            byte code = _graphQLData[_position];

            while (GraphQLConstants.IsWhitespace(code))
            {
                if (code == GraphQLConstants.NewLine)
                {
                    int next = _position + 1;
                    if (next < _length
                        && _graphQLData[next] == GraphQLConstants.Return)
                    {
                        _position = next;
                    }
                    NewLine();
                }
                else if (code == GraphQLConstants.Return)
                {
                    int next = _position + 1;
                    if (next < -_length
                        && _graphQLData[next] == GraphQLConstants.NewLine)
                    {
                        _position = next;
                    }
                    NewLine();
                }

                _position++;

                if (IsEndOfStream())
                {
                    return;
                }

                code = _graphQLData[_position];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SkipBoml()
        {
            byte code = _graphQLData[_position];

            if (code == 239)
            {
                if (_graphQLData[_position + 1] == 187
                    && _graphQLData[_position + 2] == 191)
                {
                    _position += 3;
                }
            }

            if (code == 254)
            {
                if (_graphQLData[_position + 1] == 255)
                {
                    _position += 2;
                }
            }
        }

        /// <summary>
        /// Sets the state to a new line.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NewLine()
        {
            _line++;
            _lineStart = _position;
            UpdateColumn();
        }

        /// <summary>
        /// Sets the state to a new line.
        /// </summary>
        /// <param name="lines">
        /// The number of lines to skip.
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NewLine(int lines)
        {
            if (lines < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(lines),
                    "Must be greater or equal to 1.");
            }

            _line += lines;
            _lineStart = _position;
            UpdateColumn();
        }

        /// <summary>
        /// Updates the column index.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UpdateColumn()
        {
            _column = 1 + _position - _lineStart;
        }

        /// <summary>
        /// Checks if the lexer source pointer has reached
        /// the end of the GraphQL source text.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEndOfStream()
        {
            return _position >= _length;
        }
    }
}
