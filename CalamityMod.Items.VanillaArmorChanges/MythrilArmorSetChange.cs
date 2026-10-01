using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.VanillaArmorChanges;

public class MythrilArmorSetChange : VanillaArmorChange
{
	public const int MaxManaBoost = 20;

	public const int FlareFrameSpawnDelay = 12;

	public const int FlareDamageSoftcap = 40;

	public override int? HeadPieceID => 377;

	public override int? BodyPieceID => 379;

	public override int? LegPieceID => 380;

	public override int[] AlternativeHeadPieceIDs => new int[2] { 376, 378 };

	public override string ArmorSetName => "Mythril";

	public override void UpdateSetBonusText(ref string setBonusText)
	{
		setBonusText = setBonusText + "\n" + CalamityUtils.GetText("Vanilla.Armor.SetBonus." + ArmorSetName).Format(12);
	}

	public override void ApplyHeadPieceEffect(Player player)
	{
		if (player.armor[0].type == 376)
		{
			player.statManaMax2 += 20;
		}
	}

	public override void ApplyArmorSetBonus(Player player)
	{
		player.Calamity().MythrilSet = true;
	}

	public static void OnHitEffects(NPC victim, int originalDamage, Player owner)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		if (owner.Calamity().MythrilSet && owner.Calamity().MythrilFlareSpawnCountdown <= 0)
		{
			owner.Calamity().MythrilFlareSpawnCountdown = 12;
			int flareDamage = CalamityUtils.DamageSoftCap((double)originalDamage * 0.3, 40);
			Vector2 flareSpawnPosition = victim.Center + Main.rand.NextVector2Circular(10f, 10f);
			Projectile.NewProjectile(owner.GetSource_OnHit(victim), flareSpawnPosition, Vector2.Zero, ModContent.ProjectileType<MythrilFlare>(), flareDamage, 0f, owner.whoAmI);
		}
	}
}
