using CalamityMod.CalPlayer;
using CalamityMod.Items.Armor.Silva;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class AbyssalMirror : ModItem, ILocalizedModType, IModType, IHoldShiftTooltipItem
{
	public static int AggroReduction = 450;

	public static float StandingStealthRegenBoost = 0.25f;

	public static float MovingStealthRegenBoost = 0.12f;

	public new string LocalizationCategory => "Items.Accessories";

	public bool HasFlavorTooltip => true;

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(StandingStealthRegenBoost.ToPercent(), MovingStealthRegenBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 38;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.stealthGenStandstill += StandingStealthRegenBoost;
		calamityPlayer.stealthGenMoving += MovingStealthRegenBoost;
		calamityPlayer.abyssalMirror = true;
		player.aggro -= AggroReduction;
		calamityPlayer.DodgeEffects.Add(AbyssMirrorDodge);
	}

	public string AbyssMirrorDodge(Player Player, Player.HurtInfo info)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		int abyssalMirrorDodgeIFrames = Player.ComputeDodgeIFrames();
		Player.GiveUniversalIFrames(abyssalMirrorDodgeIFrames, blink: true);
		Player.Calamity().rogueStealth += 0.5f;
		SoundEngine.PlaySound(in SilvaArmor.ActivationSound, Player.Center);
		IEntitySource source = Player.GetSource_Accessory(Player.Calamity().FindAccessory(ModContent.ItemType<AbyssalMirror>()));
		for (int i = 0; i < 10; i++)
		{
			int damage = (int)Player.GetTotalDamage<RogueDamageClass>().ApplyTo(55f);
			int lumenyl = Projectile.NewProjectile(source, Player.Center.X, Player.Center.Y, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f), ModContent.ProjectileType<AbyssalMirrorProjectile>(), damage, 0f, Player.whoAmI);
			Main.projectile[lumenyl].rotation = Main.rand.NextFloat(0f, 360f);
			Main.projectile[lumenyl].frame = Main.rand.Next(0, 4);
			if (lumenyl.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[lumenyl].DamageType = DamageClass.Generic;
			}
		}
		if (Player.whoAmI == Main.myPlayer)
		{
			NetMessage.SendData(62, -1, -1, null, Player.whoAmI, 1f);
		}
		return "abyssmirror";
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MirageMirror>().AddIngredient<InkBomb>().AddIngredient<DepthCells>(5)
			.AddIngredient<Lumenyl>(5)
			.AddTile(134)
			.Register();
	}
}
