using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Particles;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "CoreOfTheBloodGod" })]
public class ChaliceOfTheBloodGod : ModItem, ILocalizedModType, IModType
{
	internal static readonly int MinAllowedDamage;

	internal const float HealingPotionRatioForBufferClear = 0.5f;

	internal static readonly double BleedoutExponentialDecay;

	internal static readonly Color BleedoutBufferDamageTextColor;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(6, 12));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 32);
		base.Item.accessory = true;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		player.pStone = true;
		player.lifeRegen += 4;
		player.buffImmune[30] = true;
		player.buffImmune[ModContent.BuffType<BurningBlood>()] = true;
		player.buffImmune[ModContent.BuffType<HeavyBleeding>()] = true;
		player.buffImmune[ModContent.BuffType<Laceration>()] = true;
		calamityPlayer.chaliceOfTheBloodGod = true;
		calamityPlayer.chaliceHeartStyle = !hideVisual;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BloodPact>().AddIngredient(860).AddIngredient<BloodstoneCore>(5)
			.AddIngredient<AscendantSpiritEssence>(4)
			.AddTile<CosmicAnvil>()
			.Register();
	}

	internal static void HandleBleedout(Player player)
	{
		CalamityPlayer modPlayer = player.Calamity();
		if (modPlayer.chaliceOfTheBloodGod && modPlayer.chaliceBleedoutBuffer > (double)MinAllowedDamage)
		{
			double amountBledThisFrame = modPlayer.chaliceBleedoutBuffer * BleedoutExponentialDecay;
			modPlayer.chaliceDamagePointPartialProgress += amountBledThisFrame;
			int healthToLose = (int)modPlayer.chaliceDamagePointPartialProgress;
			if (healthToLose > 0)
			{
				player.statLife -= healthToLose;
				modPlayer.chaliceDamagePointPartialProgress -= healthToLose;
				if (player.statLife <= 0)
				{
					player.KillMe(PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.ChaliceOfTheBloodGod" + Main.rand.Next(1, 19)).ToNetworkText(player.name)), modPlayer.chaliceBleedoutBuffer, 0);
				}
			}
			SpawnBloodParticles(player, healthToLose);
			modPlayer.chaliceBleedoutBuffer *= 1.0 - BleedoutExponentialDecay;
			return;
		}
		if (modPlayer.chaliceBleedoutBuffer > 0.0)
		{
			int remainingDamage = (int)modPlayer.chaliceBleedoutBuffer + 1;
			player.statLife -= remainingDamage;
			SpawnBloodParticles(player, remainingDamage * 4);
			if (player.statLife <= 0)
			{
				string deathMessageKey = (modPlayer.chaliceOfTheBloodGod ? "Status.Death.ChaliceOfTheBloodGodClose" : "Status.Death.ChaliceOfTheBloodGodUnequip");
				player.KillMe(PlayerDeathReason.ByCustomReason(CalamityUtils.GetText(deathMessageKey).ToNetworkText(player.name)), modPlayer.chaliceBleedoutBuffer, 0);
			}
		}
		modPlayer.chaliceBleedoutBuffer = 0.0;
		modPlayer.chaliceDamagePointPartialProgress = 0.0;
	}

	private static void SpawnBloodParticles(Player player, int bleedDamage)
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer modPlayer = player.Calamity();
		float roughBloodCount = ((bleedDamage == 0) ? 0.4f : ((float)bleedDamage));
		int exactBloodCount = (int)roughBloodCount;
		if (Main.rand.NextFloat() < roughBloodCount - (float)exactBloodCount)
		{
			exactBloodCount++;
		}
		if (exactBloodCount > 18)
		{
			exactBloodCount = 18;
		}
		float bloodVelMult = 0.6f + MathHelper.Clamp((float)modPlayer.chaliceBleedoutBuffer * 0.01f, 0f, 3f);
		for (int i = 0; i < exactBloodCount; i++)
		{
			int bloodLifetime = Main.rand.Next(22, 36);
			float bloodScale = Main.rand.NextFloat(0.6f, 0.8f);
			Color bloodColor = Color.Lerp(Color.Red, Color.DarkRed, Main.rand.NextFloat());
			bloodColor = Color.Lerp(bloodColor, new Color(51, 22, 94), Main.rand.NextFloat(0.65f));
			if (Main.rand.NextBool(20))
			{
				bloodScale *= 2f;
			}
			float randomSpeedMultiplier = Main.rand.NextFloat(1.25f, 2.25f);
			Vector2 bloodVelocity = Main.rand.NextVector2Unit() * bloodVelMult * randomSpeedMultiplier;
			bloodVelocity.Y -= 5f;
			GeneralParticleHandler.SpawnParticle(new BloodParticle(player.Center, bloodVelocity, bloodLifetime, bloodScale, bloodColor));
		}
		for (int j = 0; j < exactBloodCount / 3; j++)
		{
			float bloodScale2 = Main.rand.NextFloat(0.2f, 0.33f);
			Color bloodColor2 = Color.Lerp(Color.Red, Color.DarkRed, Main.rand.NextFloat(0.5f, 1f));
			Vector2 bloodVelocity2 = Main.rand.NextVector2Unit() * bloodVelMult * Main.rand.NextFloat(1f, 2f);
			bloodVelocity2.Y -= 2.3f;
			GeneralParticleHandler.SpawnParticle(new BloodParticle2(player.Center, bloodVelocity2, 20, bloodScale2, bloodColor2));
		}
	}

	static ChaliceOfTheBloodGod()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		MinAllowedDamage = 5;
		BleedoutExponentialDecay = 0.0083333333333;
		BleedoutBufferDamageTextColor = new Color(230, 40, 100);
	}
}
