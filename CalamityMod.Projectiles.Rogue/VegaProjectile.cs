using System;
using CalamityMod.CalPlayer;
using CalamityMod.Systems.Mechanic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class VegaProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/Vega";

	private int SplitProjDamage => (int)((float)base.Projectile.damage * 0.6f);

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.MaxUpdates = 2;
	}

	public override void AI()
	{
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + MathHelper.ToRadians(45f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		OnHitEffects();
		_ = Main.player[base.Projectile.owner];
		if (!(base.Projectile.ai[1] > 0f))
		{
			return;
		}
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile item = enumerator.Current;
			if (item.type == ModContent.ProjectileType<VegaStar>() && item.owner == base.Projectile.owner)
			{
				item.ai[2] = target.whoAmI + 1;
				item.timeLeft = Math.Max(300, item.timeLeft);
				item.penetrate = 1;
				item.usesIDStaticNPCImmunity = false;
				item.usesLocalNPCImmunity = true;
				item.localNPCHitCooldown = 10;
				item.netUpdate = true;
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		OnHitEffects();
	}

	private void OnHitEffects()
	{
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		int onHitCount = 6;
		float spread = 20f;
		int projectileDamage = SplitProjDamage;
		float kb = 5f;
		int sparkID = ModContent.ProjectileType<VegaSpark>();
		int starID = ModContent.ProjectileType<VegaStar>();
		if (base.Projectile.Calamity().stealthStrike)
		{
			for (int i = 0; i < 1; i++)
			{
				int projID = ModContent.ProjectileType<LyraConstellation>();
				Vector2 velocity = Vector2.Zero;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, projID, (int)((float)projectileDamage * 0.8f), kb, base.Projectile.owner);
			}
		}
		for (int j = 0; j < onHitCount; j++)
		{
			int projID2 = ((j % 3 == 0) ? starID : sparkID);
			Vector2 velocity2 = base.Projectile.oldVelocity.RotateRandom(MathHelper.ToRadians(spread)) * 0.5f;
			float speed = Main.rand.NextFloat(1.5f, 2f);
			float moveDuration = Main.rand.Next(5, 15);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity2 * speed, projID2, projectileDamage, kb, base.Projectile.owner, 0f, moveDuration);
		}
		SoundEngine.PlaySound(SoundID.Item62 with
		{
			Volume = SoundID.Item62.Volume * 0.6f
		}, base.Projectile.position);
		SoundEngine.PlaySound(SoundID.Item68 with
		{
			Volume = SoundID.Item68.Volume * 0.2f
		}, base.Projectile.position);
		SoundEngine.PlaySound(SoundID.Item122 with
		{
			Volume = SoundID.Item122.Volume * 0.4f
		}, base.Projectile.position);
		for (int k = 0; k < ((base.Projectile.ai[1] != 0f || !base.Projectile.Calamity().stealthStrike) ? 1 : 3); k++)
		{
			Main.player[base.Projectile.owner].Calamity().StratusStarburst++;
			if (Main.player[base.Projectile.owner].Calamity().StratusStarburst <= CalamityPlayer.MaxStratusStarburst)
			{
				Main.player[base.Projectile.owner].Calamity().StarburstEntities.Add(new StarburstEntity(base.Projectile.Center));
			}
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		Collision.HitTiles(base.Projectile.position + base.Projectile.velocity, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		SoundEngine.PlaySound(in SoundID.Dig, base.Projectile.position);
		base.Projectile.Kill();
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Rogue/VegaGlow", (AssetRequestMode)2).Value;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)texture.Width / 2f, (float)texture.Height / 2f);
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, null, Color.White, base.Projectile.rotation, origin, 1f, (SpriteEffects)0);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 5; i++)
		{
			int dustType = Utils.SelectRandom<int>(Main.rand, 109, 111, 132);
			int dust = Dust.NewDust(base.Projectile.Center, 1, 1, dustType, base.Projectile.velocity.X / 3f, base.Projectile.velocity.Y / 3f, 0, default(Color), 1.5f);
			Main.dust[dust].noGravity = true;
		}
	}
}
