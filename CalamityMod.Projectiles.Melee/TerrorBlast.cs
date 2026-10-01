using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class TerrorBlast : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 2);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 30;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.DamageType = DamageClass.Melee;
	}

	public override bool? CanDamage()
	{
		return base.Projectile.localAI[0] > 0f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item60, base.Projectile.position);
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 400);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		for (int i = 0; i < 6; i++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 60, 0f, 0f, 100, default(Color), 1.5f);
		}
		for (int j = 0; j < 60; j++)
		{
			int scaryDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 60, 0f, 0f, 0, default(Color), 2.5f);
			Main.dust[scaryDust].noGravity = true;
			Dust obj = Main.dust[scaryDust];
			obj.velocity *= 3f;
			scaryDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 60, 0f, 0f, 100, default(Color), 1.5f);
			Dust obj2 = Main.dust[scaryDust];
			obj2.velocity *= 2f;
			Main.dust[scaryDust].noGravity = true;
		}
		base.Projectile.localAI[0] = 1f;
		base.Projectile.Damage();
	}
}
