using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class ValedictionBoomerang : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/Valediction";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 70);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 3;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (base.Projectile.soundDelay == 0)
		{
			base.Projectile.soundDelay = 8;
			SoundEngine.PlaySound(in SoundID.Item7, base.Projectile.position);
		}
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.ai[1]++;
			if (base.Projectile.ai[1] >= 60f)
			{
				base.Projectile.ai[0] = 1f;
				base.Projectile.ai[1] = 0f;
				base.Projectile.netUpdate = true;
			}
			else
			{
				CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 200f, 12f, 20f);
			}
		}
		else
		{
			float acceleration = 5f;
			Vector2 projVector = player.Center - base.Projectile.Center;
			float playerDist = ((Vector2)(ref projVector)).Length();
			if (playerDist > 3000f)
			{
				base.Projectile.Kill();
			}
			playerDist = 30f / playerDist;
			projVector.X *= playerDist;
			projVector.Y *= playerDist;
			if (base.Projectile.velocity.X < projVector.X)
			{
				base.Projectile.velocity.X += acceleration;
				if (base.Projectile.velocity.X < 0f && projVector.X > 0f)
				{
					base.Projectile.velocity.X += acceleration;
				}
			}
			else if (base.Projectile.velocity.X > projVector.X)
			{
				base.Projectile.velocity.X -= acceleration;
				if (base.Projectile.velocity.X > 0f && projVector.X < 0f)
				{
					base.Projectile.velocity.X -= acceleration;
				}
			}
			if (base.Projectile.velocity.Y < projVector.Y)
			{
				base.Projectile.velocity.Y += acceleration;
				if (base.Projectile.velocity.Y < 0f && projVector.Y > 0f)
				{
					base.Projectile.velocity.Y += acceleration;
				}
			}
			else if (base.Projectile.velocity.Y > projVector.Y)
			{
				base.Projectile.velocity.Y -= acceleration;
				if (base.Projectile.velocity.Y > 0f && projVector.Y < 0f)
				{
					base.Projectile.velocity.Y -= acceleration;
				}
			}
			if (Main.myPlayer == base.Projectile.owner)
			{
				Rectangle projHitbox = default(Rectangle);
				((Rectangle)(ref projHitbox))._002Ector((int)base.Projectile.position.X, (int)base.Projectile.position.Y, base.Projectile.width, base.Projectile.height);
				Rectangle playerHitbox = default(Rectangle);
				((Rectangle)(ref playerHitbox))._002Ector((int)player.position.X, (int)player.position.Y, player.width, player.height);
				if (((Rectangle)(ref projHitbox)).Intersects(playerHitbox))
				{
					base.Projectile.Kill();
				}
			}
		}
		base.Projectile.rotation += 0.5f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<HadopelagicPressure>(), 180);
		OnHitEffects();
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<HadopelagicPressure>(), 180);
		OnHitEffects();
	}

	private void OnHitEffects()
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		int typhoonAmt = 3;
		int typhoonDamage = (int)((float)base.Projectile.damage * 0.3f);
		if (base.Projectile.owner != Main.myPlayer || base.Projectile.numHits >= 1 || !base.Projectile.Calamity().stealthStrike)
		{
			return;
		}
		SoundEngine.PlaySound(in SoundID.Item84, base.Projectile.position);
		Vector2 velocity = default(Vector2);
		for (int typhoonCount = 0; typhoonCount < typhoonAmt; typhoonCount++)
		{
			((Vector2)(ref velocity))._002Ector((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
			while (velocity.X == 0f && velocity.Y == 0f)
			{
				((Vector2)(ref velocity))._002Ector((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
			}
			((Vector2)(ref velocity)).Normalize();
			velocity *= (float)Main.rand.Next(70, 101) * 0.1f;
			Projectile typhoon = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<NuclearFuryProjectile>(), typhoonDamage, 0f, base.Projectile.owner, 0f, 1f);
			if (typhoon.whoAmI.WithinBounds(Main.maxProjectiles))
			{
				typhoon.DamageType = RogueDamageClass.Instance;
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item21, base.Projectile.position);
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 100);
		base.Projectile.Center = base.Projectile.position;
		for (int d = 0; d < 5; d++)
		{
			int water = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 33, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[water];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[water].scale = 0.5f;
				Main.dust[water].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int i = 0; i < 8; i++)
		{
			int water2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 33, 0f, 0f, 100, default(Color), 3f);
			Main.dust[water2].noGravity = true;
			Dust obj2 = Main.dust[water2];
			obj2.velocity *= 5f;
			water2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 33, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[water2];
			obj3.velocity *= 2f;
		}
	}
}
