using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class LanternSoul : ModProjectile, ILocalizedModType, IModType
{
	public float count;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 26;
		base.Projectile.height = 26;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.sentry = true;
		base.Projectile.timeLeft = 36000;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 1f, 0f, 0f);
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= 4)
		{
			base.Projectile.frame = 0;
		}
		_ = Main.player[base.Projectile.owner];
		base.Projectile.velocity = new Vector2(0f, (float)Math.Sin((float)Math.PI * 2f * base.Projectile.ai[0] / 300f) * 0.5f);
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= 300f)
		{
			base.Projectile.ai[0] = 0f;
			base.Projectile.netUpdate = true;
		}
		int flameCount = 0;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.owner == Main.myPlayer && p.type == ModContent.ProjectileType<LanternFlame>())
			{
				flameCount++;
			}
		}
		if (flameCount >= 15)
		{
			return;
		}
		base.Projectile.ai[1] += (float)Main.rand.Next(2, 6) + 1f;
		if (base.Projectile.ai[1] >= 105f)
		{
			base.Projectile.ai[1] = 0f;
			base.Projectile.netUpdate = true;
			if (base.Projectile.owner == Main.myPlayer)
			{
				float startOffsetX = Main.rand.NextFloat(15f, 200f) * (Main.rand.NextBool() ? (-1f) : 1f);
				float startOffsetY = Main.rand.NextFloat(15f, 200f) * (Main.rand.NextBool() ? (-1f) : 1f);
				Vector2 startPos = default(Vector2);
				((Vector2)(ref startPos))._002Ector(base.Projectile.position.X + startOffsetX, base.Projectile.position.Y + startOffsetY);
				Vector2 speed = default(Vector2);
				((Vector2)(ref speed))._002Ector(0f, 0f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), startPos, speed, ModContent.ProjectileType<LanternFlame>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			}
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
