using System;
using CalamityMod.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.Systems.Graphic.PixelationSystem;

public record PixelatedDrawer(Action<Matrix> DrawAction, GeneralDrawLayer DrawLayer, BlendState DefaultBlendState);
