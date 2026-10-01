using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class EclipseMirror : ModItem, ILocalizedModType, IModType, IHoldShiftTooltipItem
{
	public new string LocalizationCategory => "Items.Accessories";

	public bool HasFlavorTooltip => true;

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 46;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.stealthGenStandstill += 0.25f;
		calamityPlayer.rogueStealthMax += 0.1f;
		calamityPlayer.eclipseMirror = true;
		calamityPlayer.stealthStrikeHalfCost = true;
		player.GetCritChance<ThrowingDamageClass>() += 6f;
		player.GetDamage<ThrowingDamageClass>() += 0.06f;
		player.aggro -= 700;
		calamityPlayer.DodgeEffects.Add(EclipseMirrorDodge);
	}

	public string EclipseMirrorDodge(Player Player, Player.HurtInfo info)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		int eclipseMirrorDodgeIFrames = Player.ComputeDodgeIFrames();
		Player.GiveUniversalIFrames(eclipseMirrorDodgeIFrames, blink: true);
		Player.Calamity().rogueStealth += 0.5f;
		SoundEngine.PlaySound(in SoundID.Item68, Player.Center);
		int eclipse = Projectile.NewProjectile(Player.GetSource_Accessory(Player.Calamity().FindAccessory(ModContent.ItemType<EclipseMirror>())), Damage: (int)Player.GetTotalDamage<RogueDamageClass>().ApplyTo(2000f), position: Player.Center, velocity: Vector2.Zero, Type: ModContent.ProjectileType<EclipseMirrorBurst>(), KnockBack: 0f, Owner: Player.whoAmI);
		if (eclipse.WithinBounds(Main.maxProjectiles))
		{
			Main.projectile[eclipse].DamageType = DamageClass.Generic;
		}
		NetMessage.SendData(62, -1, -1, null, Player.whoAmI, 1f);
		return "eclipsemirror";
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AbyssalMirror>().AddIngredient<DarkMatterSheath>().AddIngredient<DarksunFragment>(20)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
