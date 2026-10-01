using System;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

[LegacyName(new string[] { "CalamitousEssence" })]
public class AshesofAnnihilation : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(7, 6));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 123;
	}

	public override void SetDefaults()
	{
		base.Item.width = 54;
		base.Item.height = 56;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 6, 66, 66);
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public void DrawPulsingAfterimage(SpriteBatch spriteBatch, Vector2 baseDrawPosition, Rectangle frame, float baseScale)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		float pulse = Main.GlobalTimeWrappedHourly * 0.68f % 1f;
		float outwardness = pulse * baseScale * 8f;
		Color drawColor = Color.BlueViolet * (float)Math.Sqrt(1f - pulse) * 0.7f;
		((Color)(ref drawColor)).A = 0;
		float scale = baseScale * MathHelper.Lerp(1f, 1.45f, pulse);
		Vector2 drawPositionOffset = Vector2.UnitY * 2f;
		float velocity = base.Item.velocity.X * 0.2f;
		Vector2 origin = frame.Size() * 0.5f;
		for (int i = 0; i < 4; i++)
		{
			Vector2 drawPosition = baseDrawPosition + ((float)Math.PI * 2f * (float)i / 4f).ToRotationVector2() * outwardness - drawPositionOffset;
			spriteBatch.Draw(TextureAssets.Item[base.Type].Value, drawPosition, (Rectangle?)frame, drawColor, velocity, origin, scale, (SpriteEffects)0, 0f);
		}
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		DrawPulsingAfterimage(spriteBatch, position, frame, scale);
		return true;
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		Rectangle frame = Main.itemAnimations[base.Type].GetFrame(TextureAssets.Item[base.Type].Value);
		DrawPulsingAfterimage(spriteBatch, base.Item.position - Main.screenPosition + frame.Size() * 0.5f, frame, scale);
		return true;
	}

	public override void Update(ref float gravity, ref float maxFallSpeed)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		float brightness = Main.essScale * Main.rand.NextFloat(0.9f, 1.1f);
		Lighting.AddLight(base.Item.Center, 0.34f * brightness, 0.08f * brightness, 0.155f * brightness);
	}
}
