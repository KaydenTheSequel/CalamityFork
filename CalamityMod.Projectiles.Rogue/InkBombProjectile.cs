using CalamityMod.Items.Accessories;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class InkBombProjectile : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle Explode = new SoundStyle("CalamityMod/Sounds/Custom/PlantyMushMine", 3);

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 22;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 0;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 20;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		base.Projectile.velocity.Y += 0.1f;
		base.Projectile.rotation += base.Projectile.velocity.X * 0.1f;
		if (base.Projectile.timeLeft == 1)
		{
			CreateInk();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (!target.friendly)
		{
			CreateInk();
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		CreateInk();
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		CreateInk();
		return true;
	}

	private void CreateInk()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		SoundEngine.PlaySound(in SoundID.NPCHit25, base.Projectile.Center);
		for (int i = 0; i < 4; i++)
		{
			int damage = (int)player.GetTotalDamage<RogueDamageClass>().ApplyTo(InkBomb.InkDamage);
			int inkID = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Main.rand.NextVector2Circular(2f, 2f), ModContent.ProjectileType<InkCloud>(), damage, 7f, base.Projectile.owner, Main.rand.Next(3) + 1);
			Main.projectile[inkID].timeLeft += Main.rand.Next(-15, 16);
		}
		base.Projectile.Kill();
	}
}
