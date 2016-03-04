// ==========================================================================
// TextListBehavior.cs
// Hercules Mindmap App
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System.Linq;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GP.Utils.UI.Interactivity
{
    /// <summary>
    /// Behavior to insert lists into the RichEditBox automatically.
    /// </summary>
    public sealed class TextListBehavior : Behavior<RichEditBox>
    {
        private const int TabSize = 15;

        private sealed class Pattern
        {
            public readonly string TextReturn;

            public readonly int TextLength;

            public readonly MarkerType MarkerType;

            public Pattern(MarkerType markerType, string text)
            {
                MarkerType = markerType;

                TextReturn = text + " \r";
                TextLength = text.Length;
            }
        }

        private static readonly Pattern[] Patterns =
        {
            new Pattern(MarkerType.Arabic, "1)"),
            new Pattern(MarkerType.Arabic, "1."),
            new Pattern(MarkerType.UppercaseRoman, "I)"),
            new Pattern(MarkerType.UppercaseRoman, "I."),
            new Pattern(MarkerType.LowercaseEnglishLetter, "a)"),
            new Pattern(MarkerType.LowercaseEnglishLetter, "a."),
            new Pattern(MarkerType.UppercaseEnglishLetter, "A)"),
            new Pattern(MarkerType.UppercaseEnglishLetter, "A."),
            new Pattern(MarkerType.Bullet, "*")
        };

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached()
        {
            AssociatedElement.KeyDown += AssociatedElement_KeyDown;
            AssociatedElement.TextChanged += AssociatedElement_TextChanged;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
        protected override void OnDetaching()
        {
            AssociatedElement.KeyDown -= AssociatedElement_KeyDown;
            AssociatedElement.TextChanged -= AssociatedElement_TextChanged;
        }

        private void AssociatedElement_TextChanged(object sender, RoutedEventArgs e)
        {
            ITextDocument document = AssociatedElement.Document;

            int position = document.Selection.StartPosition;

            ITextRange range = document.GetRange(position, position);

            if (range == null)
            {
                return;
            }

            range.Expand(TextRangeUnit.Line);

            ITextParagraphFormat format = range.ParagraphFormat;

            if (format.ListType != MarkerType.None)
            {
                return;
            }

            Pattern pattern = Patterns.FirstOrDefault(p => range.Text == p.TextReturn);

            if (pattern == null)
            {
                return;
            }

            format.ListType = pattern.MarkerType;
            format.ListStart = 1;
            format.ListTab = TabSize;
            format.ListLevelIndex = 1;

            range.Delete(TextRangeUnit.Character, pattern.TextLength);
        }

        private void AssociatedElement_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key != VirtualKey.Tab)
            {
                return;
            }

            ITextDocument document = AssociatedElement.Document;

            int position = document.Selection.StartPosition;

            ITextRange range = document.GetRange(position, position);

            if (range == null)
            {
                return;
            }

            range.Expand(TextRangeUnit.Line);

            ITextParagraphFormat format = range.ParagraphFormat;

            if (format.ListType == MarkerType.None)
            {
                return;
            }

            if (IsShiftKeyPressed())
            {
                if (format.LeftIndent >= TabSize)
                {
                    format.SetIndents(0, format.LeftIndent - TabSize, format.RightIndent);
                }
            }
            else
            {
                if (format.LeftIndent < TabSize * 5)
                {
                    format.SetIndents(0, format.LeftIndent + TabSize, format.RightIndent);
                }
            }

            document.Selection.MoveRight(TextRangeUnit.Character, -1, false);
            document.Selection.MoveRight(TextRangeUnit.Character, 1, false);
        }

        private static bool IsShiftKeyPressed()
        {
            return (Window.Current.CoreWindow.GetKeyState(VirtualKey.Shift) & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down;
        }
    }
}
