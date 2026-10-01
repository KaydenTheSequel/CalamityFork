using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated;

[GeneratedCode("System.Text.RegularExpressions.Generator", "8.0.14.7010")]
[SkipLocalsInit]
internal sealed class _003CRegexGenerator_g_003EF7EF839F8623C6609A374476407935905F891DC2675E6D6432167A284150137EE__CalamityDialogueFileRegex_0 : Regex
{
	private sealed class RunnerFactory : RegexRunnerFactory
	{
		private sealed class Runner : RegexRunner
		{
			protected override void Scan(ReadOnlySpan<char> inputSpan)
			{
				while (TryFindNextPossibleStartingPosition(inputSpan) && !TryMatchAtCurrentPosition(inputSpan) && runtextpos != inputSpan.Length)
				{
					runtextpos++;
					if (_003CRegexGenerator_g_003EF7EF839F8623C6609A374476407935905F891DC2675E6D6432167A284150137EE__Utilities.s_hasTimeout)
					{
						CheckTimeout();
					}
				}
			}

			private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
			{
				int pos = runtextpos;
				if (pos <= inputSpan.Length - 15)
				{
					int i = inputSpan.Slice(pos).IndexOf("dialogue.".AsSpan(), StringComparison.OrdinalIgnoreCase);
					if (i >= 0)
					{
						runtextpos = pos + i;
						return true;
					}
				}
				runtextpos = inputSpan.Length;
				return false;
			}

			private bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
			{
				int pos = runtextpos;
				int matchStart = pos;
				int lazyloop_pos = 0;
				ReadOnlySpan<char> slice = inputSpan.Slice(pos);
				if ((uint)slice.Length < 9u || !slice.StartsWith("dialogue.".AsSpan(), StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
				if ((uint)slice.Length < 10u || slice[9] == '\n')
				{
					return false;
				}
				pos += 10;
				slice = inputSpan.Slice(pos);
				lazyloop_pos = pos;
				while (true)
				{
					if ((uint)slice.Length >= 5u && slice.StartsWith(".json".AsSpan(), StringComparison.OrdinalIgnoreCase))
					{
						if ((uint)slice.Length > 5u && (slice[5] | 0x20) == 99)
						{
							slice = slice.Slice(1);
							pos++;
						}
						if (6 >= slice.Length && (5 >= slice.Length || slice[5] == '\n'))
						{
							break;
						}
					}
					if (_003CRegexGenerator_g_003EF7EF839F8623C6609A374476407935905F891DC2675E6D6432167A284150137EE__Utilities.s_hasTimeout)
					{
						CheckTimeout();
					}
					pos = lazyloop_pos;
					slice = inputSpan.Slice(pos);
					if (slice.IsEmpty || slice[0] == '\n')
					{
						return false;
					}
					pos++;
					slice = inputSpan.Slice(pos);
					lazyloop_pos = slice.IndexOfAny('\n', '.');
					if ((uint)lazyloop_pos >= (uint)slice.Length || slice[lazyloop_pos] == '\n')
					{
						return false;
					}
					pos += lazyloop_pos;
					slice = inputSpan.Slice(pos);
					lazyloop_pos = pos;
				}
				Capture(0, matchStart, runtextpos = pos + 5);
				return true;
			}
		}

		protected override RegexRunner CreateInstance()
		{
			return new Runner();
		}
	}

	internal static readonly _003CRegexGenerator_g_003EF7EF839F8623C6609A374476407935905F891DC2675E6D6432167A284150137EE__CalamityDialogueFileRegex_0 Instance = new _003CRegexGenerator_g_003EF7EF839F8623C6609A374476407935905F891DC2675E6D6432167A284150137EE__CalamityDialogueFileRegex_0();

	private _003CRegexGenerator_g_003EF7EF839F8623C6609A374476407935905F891DC2675E6D6432167A284150137EE__CalamityDialogueFileRegex_0()
	{
		pattern = "Dialogue\\..+?\\.jsonc?$";
		roptions = RegexOptions.IgnoreCase;
		Regex.ValidateMatchTimeout(_003CRegexGenerator_g_003EF7EF839F8623C6609A374476407935905F891DC2675E6D6432167A284150137EE__Utilities.s_defaultTimeout);
		internalMatchTimeout = _003CRegexGenerator_g_003EF7EF839F8623C6609A374476407935905F891DC2675E6D6432167A284150137EE__Utilities.s_defaultTimeout;
		factory = new RunnerFactory();
		capsize = 1;
	}
}
