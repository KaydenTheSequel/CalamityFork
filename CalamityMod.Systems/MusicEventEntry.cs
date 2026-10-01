using System;

namespace CalamityMod.Systems;

public record MusicEventEntry(string Id, int Song, TimeSpan Length, TimeSpan IntroSilence, TimeSpan OutroSilence, Func<bool> ShouldPlay, Func<bool> Enabled);
