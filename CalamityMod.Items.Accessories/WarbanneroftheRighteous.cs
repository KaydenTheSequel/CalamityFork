using System;
using CalamityMod.CalPlayer;
using CalamityMod.Cooldowns;
using CalamityMod.NPCs;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[AutoloadEquip(new EquipType[] { EquipType.Balloon })]
[LegacyName(new string[] { "SamuraiBadge" })]
[LegacyName(new string[] { "WarbanneroftheSun" })]
public class WarbanneroftheRighteous : ModItem, ILocalizedModType, IModType
{
	internal const float MaxBonus = 0.3f;

	internal const float MaxDistance = 700f;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(6, 5));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 78;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.accessory = true;
		base.Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.WarbanneroftheRighteous = true;
		calamityPlayer.warbannerGlow = !hideVisual;
		if (player.ownedProjectileCounts[ModContent.ProjectileType<WarbannerLight>()] < 1 && !hideVisual && !player.dead)
		{
			Projectile.NewProjectileDirect(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<WarbannerLight>(), 0, 0f, player.whoAmI);
		}
		int maxValue = 30;
		float bonus = CalculateBonus(player) - 0.15f;
		float displayBonus = (int)((bonus + 0.15f) * 100f);
		if (player.Calamity().cooldowns.TryGetValue(WarbanneroftheRighteousBuff.ID, out var cooldown))
		{
			cooldown.timeLeft = maxValue - (int)displayBonus;
		}
		else
		{
			player.AddCooldown(WarbanneroftheRighteousBuff.ID, maxValue);
		}
		player.Calamity().warbannerDamageMult = bonus;
		int targetCount = 0;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			if (enumerator.Current.Calamity().warbannerBurnMarked)
			{
				targetCount++;
			}
		}
		for (int index = 0; index < Main.npc.Length; index++)
		{
			NPC nPC = Main.npc[index];
			float generousHitboxWidth = Math.Max((float)nPC.Hitbox.Width / 2f, (float)nPC.Hitbox.Height / 2f) + 100f;
			float intensity = Utils.Remap(player.Center.Distance(nPC.Center), 700f + generousHitboxWidth, generousHitboxWidth, 1f, 3f);
			if (nPC.Center.Distance(player.Center) < 700f + generousHitboxWidth && (nPC.IsAnEnemy(allowStatues: true, checkDead: true, checkDamage: false) || nPC.type == ModContent.NPCType<SuperDummyNPC>()) && !nPC.dontTakeDamage)
			{
				float minDamageMult = 0.1f;
				int maxTargets = 7;
				float damageMult = Utils.Remap(targetCount, maxTargets, 1f, minDamageMult, 1f);
				CalamityGlobalNPC modNPC = nPC.Calamity();
				if (!modNPC.warbannerBurnMarked)
				{
					modNPC.warbannerBurnDirection = player.Center.DirectionTo(nPC.Center);
					modNPC.warbannerBurnMarked = true;
					modNPC.warbannerBurnTimer = 180;
				}
				if (modNPC.warbannerBurnMarked)
				{
					modNPC.warbannerBurnIntensity = intensity;
					modNPC.warbannerBurnDirection = player.Center.DirectionTo(nPC.Center);
					int burnDamage = (int)player.GetBestClassDamage().ApplyTo(15f * damageMult);
					modNPC.warbannerBurnDamage = burnDamage;
					modNPC.warbannerBurnStacks++;
					modNPC.warbannerBurnTimer = 180;
					modNPC.warbannerBurnHideEffects = hideVisual;
				}
			}
		}
	}

	private static float CalculateBonus(Player player)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		float bonus = 0f;
		NPC closestTarget = player.Center.ClosestNPCAt(4900f);
		if (closestTarget != null)
		{
			float generousHitboxWidth = Math.Max((float)closestTarget.Hitbox.Width / 2f, (float)closestTarget.Hitbox.Height / 2f) + 100f;
			return Utils.Remap(player.Center.Distance(closestTarget.Center), 700f + generousHitboxWidth, generousHitboxWidth, 0f, 0.3f);
		}
		return 0f;
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawInventoryCustomScale(spriteBatch, TextureAssets.Item[base.Type].Value, position, frame, drawColor, itemColor, origin, scale, 0.6f, new Vector2(0f, -2f), (SpriteEffects)0);
		return false;
	}
}
