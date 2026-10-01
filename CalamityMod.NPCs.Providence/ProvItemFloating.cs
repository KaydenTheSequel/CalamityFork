using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Events;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Accessories.Wings;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.Dyes;
using CalamityMod.Items.LoreItems;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Furniture.BossRelics;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using CalamityMod.Items.Potions.Food;
using CalamityMod.Items.SummonItems;
using CalamityMod.Items.TreasureBags;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Providence;

public class ProvItemFloating : GlobalItem
{
	public static readonly List<int> FlameItemTypes = new List<int>();

	public float HolyFlame;

	public float FlameTimer;

	public bool ProviWasEnraged;

	public override bool InstancePerEntity => true;

	public override void SetStaticDefaults()
	{
		FlameItemTypes.AddRange(new global::_003C_003Ez__ReadOnlyArray<int>(new int[24]
		{
			ModContent.ItemType<UnholyEssence>(),
			ModContent.ItemType<DivineGeode>(),
			ModContent.ItemType<MarkofProvidence>(),
			ModContent.ItemType<ProvidenceBag>(),
			ModContent.ItemType<HolyCollider>(),
			ModContent.ItemType<BurningRevelation>(),
			ModContent.ItemType<BlissfulBombardier>(),
			ModContent.ItemType<TelluricGlare>(),
			ModContent.ItemType<PurgeGuzzler>(),
			ModContent.ItemType<DazzlingStabberStaff>(),
			ModContent.ItemType<MoltenAmputator>(),
			ModContent.ItemType<PristineFury>(),
			ModContent.ItemType<ElysianWings>(),
			ModContent.ItemType<ElysianAegis>(),
			ModContent.ItemType<BlazingCore>(),
			ModContent.ItemType<ProfanedSoulCrystal>(),
			ModContent.ItemType<ProfanedMoonlightDye>(),
			ModContent.ItemType<ProvidenceMask>(),
			ModContent.ItemType<ThankYouPainting>(),
			ModContent.ItemType<ProvidenceTrophy>(),
			ModContent.ItemType<ProvidenceRelic>(),
			ModContent.ItemType<LoreProvidence>(),
			ModContent.ItemType<AscendantSpiritEssence>(),
			ModContent.ItemType<BlasphemousDonut>()
		}));
	}

	public override bool AppliesToEntity(Item entity, bool lateInstantiation)
	{
		if (FlameItemTypes.Contains(entity.type))
		{
			return true;
		}
		return false;
	}

	public override void OnSpawn(Item item, IEntitySource source)
	{
		if (!BossRushEvent.BossRushActive && source is EntitySource_Loot { Entity: NPC { ModNPC: Providence provi } })
		{
			HolyFlame = 2f;
			ProviWasEnraged = provi.hasBeenGivenFullPower;
		}
	}

	public override void NetSend(Item item, BinaryWriter writer)
	{
		writer.Write((Half)HolyFlame);
		writer.Write(ProviWasEnraged);
	}

	public override void NetReceive(Item item, BinaryReader reader)
	{
		HolyFlame = (float)reader.ReadHalf();
		ProviWasEnraged = reader.ReadBoolean();
	}

	public override void Update(Item item, ref float gravity, ref float maxFallSpeed)
	{
		float clp = MathHelper.Clamp(HolyFlame, 0f, 1f);
		maxFallSpeed *= MathHelper.Lerp(1f, 0f, clp);
		if (maxFallSpeed > 0.3f)
		{
			HolyFlame *= 0.95f;
		}
		HolyFlame *= 0.9975f;
		HolyFlame = MathHelper.Clamp(HolyFlame, 0f, 2f);
		if (item.beingGrabbed)
		{
			HolyFlame = 0f;
		}
	}

	public override void UpdateInventory(Item item, Player player)
	{
		HolyFlame = 0f;
	}

	public override bool PreDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		FlameTimer++;
		if (HolyFlame > 0f)
		{
			Color lColor = lightColor * MathHelper.Lerp(1f, 0f, MathHelper.Clamp(HolyFlame - 1f, 0f, 1f));
			((Color)(ref lColor)).A = byte.MaxValue;
			Color alph = default(Color);
			((Color)(ref alph))._002Ector(255f * (HolyFlame / 2f), 255f * (HolyFlame / 2f), 0f, 0f);
			Color alph2 = default(Color);
			((Color)(ref alph2))._002Ector(155f * (HolyFlame / 2f), 0f, 0f, 0f);
			if (ProviWasEnraged)
			{
				((Color)(ref lColor)).B = byte.MaxValue;
				((Color)(ref alph))._002Ector(0f, 255f * (HolyFlame / 2f), 255f * (HolyFlame / 2f), 0f);
				((Color)(ref alph2))._002Ector(0f, 0f, 155f * (HolyFlame / 2f), 0f);
			}
			Rectangle frame = Item.GetDrawHitbox(item.type, Main.LocalPlayer);
			if (HolyFlame > 0f)
			{
				for (float i = 0f; i < 360f; i += 90f)
				{
					Main.EntitySpriteDraw(TextureAssets.Item[item.type].Value, item.Center - Main.screenPosition + Utils.RotatedBy(new Vector2(4f * MathHelper.Clamp(HolyFlame, 0f, 1f), 0f), (double)MathHelper.ToRadians(i), default(Vector2)), frame, alph, rotation, frame.Size() / 2f, scale, (SpriteEffects)0);
				}
			}
			float maxIterations = 20f;
			for (float i2 = 0f; i2 < maxIterations; i2++)
			{
				Main.EntitySpriteDraw(TextureAssets.Item[item.type].Value, item.Center - Main.screenPosition + new Vector2((float)Math.Sin(FlameTimer / 20f + (float)item.whoAmI * 13098.125f - i2 / 5f) * i2, (0f - i2) * 1.5f) * HolyFlame, frame, Color.Lerp(alph, alph2, i2 / maxIterations), rotation, frame.Size() / 2f, MathHelper.Lerp(scale, 0f, i2 / maxIterations * (HolyFlame / 2f)), (SpriteEffects)0);
			}
			Main.EntitySpriteDraw(TextureAssets.Item[item.type].Value, item.Center - Main.screenPosition, frame, lColor, rotation, frame.Size() / 2f, scale, (SpriteEffects)0);
			return false;
		}
		return base.PreDrawInWorld(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
	}
}
