using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Gores.Trees;

public class SulphurLeaf : ModGore
{
	public override void SetStaticDefaults()
	{
		ChildSafety.SafeGore[base.Type] = true;
	}

	public override void OnSpawn(Gore gore, IEntitySource source)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		ChildSafety.SafeGore[gore.type] = true;
		gore.velocity = new Vector2(Main.rand.NextFloat() - 0.5f, Main.rand.NextFloat() * ((float)Math.PI * 2f));
		gore.numFrames = 8;
		gore.frame = (byte)Main.rand.Next(8);
		gore.frameCounter = (byte)Main.rand.Next(8);
		base.UpdateType = 910;
	}
}
