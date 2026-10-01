using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.VanillaArmorChanges;

public class RainArmorSetChange : VanillaArmorChange
{
	public override int? HeadPieceID => 1135;

	public override int? BodyPieceID => 1136;

	public override int? LegPieceID => null;

	public override string ArmorSetName => "Rain";

	public override void UpdateSetBonusText(ref string setBonusText)
	{
		setBonusText = CalamityUtils.GetTextValue("Vanilla.Armor.SetBonus." + ArmorSetName) ?? "";
	}

	public override void ApplyArmorSetBonus(Player player)
	{
		player.Calamity().rainSet = true;
		player.autoJump = true;
		player.jumpSpeedBoost += 1.2f;
	}

	public static void SpawnRainArmorJump(Player Player)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		bool rainBoost = (double)Player.Center.Y < Main.worldSurface * 16.0 && Main.raining;
		int damage = (int)Player.GetBestClassDamage().ApplyTo(30f * (rainBoost ? 2f : 1f));
		Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center + Vector2.UnitY * 26f, Vector2.Zero, ModContent.ProjectileType<PuddleSplash>(), damage, 0f, Main.myPlayer);
	}
}
