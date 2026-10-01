using System;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

public class ExoPrism : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 121;
	}

	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 52;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 7);
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public void DrawBackAfterimage(SpriteBatch spriteBatch, Vector2 baseDrawPosition, Rectangle frame, float baseScale)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		float pulse = Main.GlobalTimeWrappedHourly * 0.75f % 1f;
		float outwardnessFactor = MathHelper.Lerp(0.9f, 1.3f, pulse);
		Color drawColor = Color.MintCream * (1f - pulse) * 0.27f;
		((Color)(ref drawColor)).A = 0;
		float scale = baseScale * outwardnessFactor;
		float velocity = base.Item.velocity.X * 0.2f;
		Vector2 origin = frame.Size() * 0.5f;
		for (int i = 0; i < 4; i++)
		{
			Vector2 drawPosition = baseDrawPosition + ((float)Math.PI * 2f * (float)i / 4f).ToRotationVector2() * 4f;
			spriteBatch.Draw(TextureAssets.Item[base.Type].Value, drawPosition, (Rectangle?)frame, drawColor, velocity, origin, scale, (SpriteEffects)0, 0f);
		}
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		Rectangle frame = TextureAssets.Item[base.Type].Value.Frame();
		DrawBackAfterimage(spriteBatch, base.Item.position - Main.screenPosition + frame.Size() * 0.5f, frame, scale);
		return true;
	}

	public override void Update(ref float gravity, ref float maxFallSpeed)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		float brightness = Main.essScale * Main.rand.NextFloat(0.9f, 1.1f);
		Lighting.AddLight(base.Item.Center, 0.6f * brightness, 0.64f * brightness, 0.6f * brightness);
		if (Main.rand.NextBool(3))
		{
			Dust dust = Dust.NewDustDirect(base.Item.position, (int)((float)base.Item.width * base.Item.scale), (int)((float)base.Item.height * base.Item.scale * 0.6f), 204);
			dust.velocity = Vector2.Lerp(Main.rand.NextVector2Unit(), -Vector2.UnitY, 0.5f) * Main.rand.NextFloat(1.2f, 1.8f);
			dust.fadeIn = 0.7f;
			dust.noGravity = true;
		}
	}
}
