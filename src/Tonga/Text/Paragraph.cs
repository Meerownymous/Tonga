

using System;
using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Text
{
    /// <summary>
    /// A paragraph which seperates the given lines by a carriage return.
    /// </summary>
    public sealed class Paragraph : TextEnvelope
    {
        #region string head, IEnumerable<string> lines, params string[] tail
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head, IEnumerable<string> lines, params string[] tail) : this(
            new Mapped<string, IText>(
                line => AsText._(line),
                new Joined<string>(
                    new AsEnumerable<string>(head),
                    lines,
                    new AsEnumerable<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, IEnumerable<string> lines, params string[] tail) : this(
            new Mapped<string, IText>(
                line => AsText._(line),
                new Joined<string>(
                    new AsEnumerable<string>(head1, head2),
                    lines,
                    new AsEnumerable<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, IEnumerable<string> lines, params string[] tail) : this(
            new Mapped<string, IText>(
                line => AsText._(line),
                new Joined<string>(
                    new AsEnumerable<string>(head1, head2, head3),
                    lines,
                    new AsEnumerable<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, IEnumerable<string> lines, params string[] tail) : this(
            new Mapped<string, IText>(
                line => AsText._(line),
                new Joined<string>(
                    new AsEnumerable<string>(head1, head2, head3, head4),
                    lines,
                    new AsEnumerable<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, string head5, IEnumerable<string> lines, params string[] tail) : this(
            new Mapped<string, IText>(
                line => AsText._(line),
                new Joined<string>(
                    new AsEnumerable<string>(head1, head2, head3, head4, head5),
                    lines,
                    new AsEnumerable<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, string head5, string head6, IEnumerable<string> lines, params string[] tail) : this(
            new Mapped<string, IText>(
                line => AsText._(line),
                new Joined<string>(
                    new AsEnumerable<string>(head1, head2, head3, head4, head5, head6),
                    lines,
                    new AsEnumerable<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, string head5, string head6, string head7, IEnumerable<string> lines, params string[] tail) : this(
            new Mapped<string, IText>(
                line => AsText._(line),
                new Joined<string>(
                    new AsEnumerable<string>(head1, head2, head3, head4, head5, head6, head7),
                    lines,
                    new AsEnumerable<string>(tail)
                )
            )
        )
        { }
        #endregion

        #region IText head1, IEnumerable<string> lines, params string[] tail
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IEnumerable<string> lines, params string[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head1),
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new Joined<string>(
                        lines,
                        new AsEnumerable<string>(tail)
                    )
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IEnumerable<string> lines, params string[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head1, head2),
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new Joined<string>(
                        lines,
                        new AsEnumerable<string>(tail)
                    )
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IEnumerable<string> lines, params string[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head1, head2, head3),
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new Joined<string>(
                        lines,
                        new AsEnumerable<string>(tail)
                    )
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IEnumerable<string> lines, params string[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head1, head2, head3, head4),
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new Joined<string>(
                        lines,
                        new AsEnumerable<string>(tail)
                    )
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IText head5, IEnumerable<string> lines, params string[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head1, head2, head3, head4, head5),
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new Joined<string>(
                        lines,
                        new AsEnumerable<string>(tail)
                    )
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IText head5, IText head6, IEnumerable<string> lines, params string[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head1, head2, head3, head4, head5, head6),
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new Joined<string>(
                        lines,
                        new AsEnumerable<string>(tail)
                    )
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IText head5, IText head6, IText head7, IEnumerable<string> lines, params string[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head1, head2, head3, head4, head5, head6, head7),
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new Joined<string>(
                        lines,
                        new AsEnumerable<string>(tail)
                    )
                )
            )
        )
        { }
        #endregion

        #region string head, IEnumerable<IText> lines, params string[] tail
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(head)
                ),
                lines,
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(head1, head2)
                ),
                lines,
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(head1, head2, head3)
                ),
                lines,
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(head1, head2, head3, head4)
                ),
                lines,
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, string head5, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(head1, head2, head3, head4, head5)
                ),
                lines,
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, string head5, string head6, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(head1, head2, head3, head4, head5, head6)
                ),
                lines,
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, string head5, string head6, string head7, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(head1, head2, head3, head4, head5, head6, head7)
                ),
                lines,
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(tail)
                )
            )
        )
        { }
        #endregion

        #region IText head, IEnumerable<IText> lines, params string[] tail
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head),
                lines,
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head1, head2),
                lines,
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head1, head2, head3),
                lines,
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head1, head2, head3, head4),
                lines,
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IText head5, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head1, head2, head3, head4, head5),
                lines,
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IText head5, IText head6, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head1, head2, head3, head4, head5, head6),
                lines,
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IText head5, IText head6, IText head7, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head1, head2, head3, head4, head5, head6, head7),
                lines,
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(tail)
                )
            )
        )
        { }
        #endregion

        #region string head, IEnumerable<string> lines, params IText[] tail
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new Joined<string>(
                        new AsEnumerable<string>(head),
                        lines
                    )
                ),
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new Joined<string>(
                        new AsEnumerable<string>(head1, head2),
                        lines
                    )
                ),
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new Joined<string>(
                        new AsEnumerable<string>(head1, head2, head3),
                        lines
                    )
                ),
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new Joined<string>(
                        new AsEnumerable<string>(head1, head2, head3, head4),
                        lines
                    )
                ),
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, string head5, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new Joined<string>(
                        new AsEnumerable<string>(head1, head2, head3, head4, head5),
                        lines
                    )
                ),
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, string head5, string head6, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new Joined<string>(
                        new AsEnumerable<string>(head1, head2, head3, head4, head5, head6),
                        lines
                    )
                ),
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, string head5, string head6, string head7, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new Joined<string>(
                        new AsEnumerable<string>(head1, head2, head3, head4, head5, head6, head7),
                        lines
                    )
                ),
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        #endregion

        #region IText head, IEnumerable<string> lines, params IText[] tail
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head),
                new Mapped<string, IText>(
                    line => AsText._(line),
                    lines
                ),
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head1, head2),
                new Mapped<string, IText>(
                    line => AsText._(line),
                    lines
                ),
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head1, head2, head3),
                new Mapped<string, IText>(
                    line => AsText._(line),
                    lines
                ),
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head1, head2, head3, head4),
                new Mapped<string, IText>(
                    line => AsText._(line),
                    lines
                ),
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IText head5, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head1, head2, head3, head4, head5),
                new Mapped<string, IText>(
                    line => AsText._(line),
                    lines
                ),
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IText head5, IText head6, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head1, head2, head3, head4, head5, head6),
                new Mapped<string, IText>(
                    line => AsText._(line),
                    lines
                ),
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IText head5, IText head6, IText head7, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head1, head2, head3, head4, head5, head6, head7),
                new Mapped<string, IText>(
                    line => AsText._(line),
                    lines
                ),
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        #endregion

        #region string head, IEnumerable<IText> lines, params IText[] tail
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(head)
                ),
                lines,
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(head1, head2)
                ),
                lines,
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(head1, head2, head3)
                ),
                lines,
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(head1, head2, head3, head4)
                ),
                lines,
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, string head5, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(head1, head2, head3, head4, head5)
                ),
                lines,
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, string head5, string head6, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(head1, head2, head3, head4, head5, head6)
                ),
                lines,
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, string head5, string head6, string head7, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => AsText._(line),
                    new AsEnumerable<string>(head1, head2, head3, head4, head5, head6, head7)
                ),
                lines,
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        #endregion

        #region IText head, IEnumerable<IText> lines, params IText[] tail
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head),
                lines,
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head1, head2),
                lines,
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head1, head2, head3),
                lines,
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head1, head2, head3, head4),
                lines,
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IText head5, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head1, head2, head3, head4, head5),
                lines,
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IText head5, IText head6, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head1, head2, head3, head4, head5, head6),
                lines,
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IText head5, IText head6, IText head7, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new AsEnumerable<IText>(head1, head2, head3, head4, head5, head6, head7),
                lines,
                new AsEnumerable<IText>(tail)
            )
        )
        { }
        #endregion


        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(params string[] lines) : this(Mapped._(AsText._, lines))
        { }

        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IEnumerable<string> lines) : this(
            Mapped._(
                AsText._,
                lines
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(params IText[] lines) : this(AsEnumerable._(lines))
        { }

        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IEnumerable<IText> lines) : base(
            Joined._(Environment.NewLine, lines)
        )
        { }
    }
}
