using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class EndoCooperLimbs : ModProjectile, ILocalizedModType, IModType
{
	private int AttackMode;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.minion = true;
		base.Projectile.minionSlots = 0f;
		base.Projectile.netImportant = true;
		base.Projectile.width = 90;
		base.Projectile.height = 90;
		base.Projectile.timeLeft = 18000;
		base.Projectile.timeLeft *= 5;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.extraUpdates = 1;
		base.Projectile.coldDamage = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		Projectile body = Main.projectile[(int)base.Projectile.ai[1]];
		bool num = base.Projectile.type == ModContent.ProjectileType<EndoCooperLimbs>();
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		player.AddBuff(ModContent.BuffType<EndoCooperBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				modPlayer.endoCooper = false;
			}
			if (modPlayer.endoCooper)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0]++;
			AttackMode = (int)base.Projectile.ai[0];
			base.Projectile.ai[0] = 0f;
			SpawnDust();
		}
		AttackMode = (int)body.localAI[1];
		if (AttackMode == 3)
		{
			base.Projectile.rotation += 0.015f;
		}
		else
		{
			float rotateratio = 0.007f;
			float rotation = (Math.Abs(body.velocity.X) + Math.Abs(body.velocity.Y)) * rotateratio;
			base.Projectile.rotation += rotation * (float)body.direction;
		}
		if (body.type != ModContent.ProjectileType<EndoCooperBody>() || !body.active)
		{
			base.Projectile.Kill();
		}
		base.Projectile.Center = body.Center;
		if (base.Projectile.ai[0] == 1f && AttackMode == 1)
		{
			base.Projectile.alpha = 255;
			SpawnShards();
			base.Projectile.ai[0] = 2f;
			base.Projectile.netUpdate = true;
		}
		if (base.Projectile.ai[0] == 3f)
		{
			SpawnDust();
			base.Projectile.alpha = 255;
			base.Projectile.ai[0] = 0f;
		}
		if (base.Projectile.ai[0] == 4f)
		{
			SpawnFlames();
			base.Projectile.ai[0] = 0f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		SpawnDust();
		SoundEngine.PlaySound(in SoundID.NPCHit5, base.Projectile.Center);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 2f)
		{
			return false;
		}
		int dye = Main.player[base.Projectile.owner]?.cMinion ?? 0;
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 1, null, drawCentered: true, shrink: false, dye);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] != 2f)
		{
			Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/EndoCooperLimbs_Glow", (AssetRequestMode)2).Value;
			Rectangle frame = default(Rectangle);
			((Rectangle)(ref frame))._002Ector(0, 0, texture.Width, texture.Height);
			Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, frame, Color.LightSkyBlue, base.Projectile.rotation, base.Projectile.Size / 2f, 1f, (SpriteEffects)0);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GlacialState>(), 60);
		target.AddBuff(ModContent.BuffType<Voidfrost>(), 180);
	}

	public override bool? CanDamage()
	{
		return AttackMode == 2;
	}

	private void SpawnShards()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		SpawnDust();
		SoundEngine.PlaySound(in SoundID.NPCHit5, base.Projectile.Center);
		if (AttackMode == 1)
		{
			for (int i = 0; i < 360; i += 60)
			{
				Vector2 pspeed1 = Utils.RotatedBy(new Vector2(Main.rand.NextFloat(3f, 8f), Main.rand.NextFloat(3f, 8f)), (double)MathHelper.ToRadians((float)(i + 20) + MathHelper.ToDegrees(base.Projectile.rotation)), default(Vector2));
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, pspeed1, ModContent.ProjectileType<EndoIceShard>(), base.Projectile.damage, 1f, base.Projectile.owner);
				Vector2 pspeed2 = pspeed1.RotatedBy(MathHelper.ToRadians((float)Main.rand.Next(5, 13))) * Main.rand.NextFloat(0.6f, 1.3f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, pspeed2, ModContent.ProjectileType<EndoIceShard>(), base.Projectile.damage, 1f, base.Projectile.owner);
				Vector2 pspeed3 = pspeed1.RotatedBy(MathHelper.ToRadians((float)Main.rand.Next(-12, -1))) * Main.rand.NextFloat(0.6f, 1.3f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, pspeed3, ModContent.ProjectileType<EndoIceShard>(), base.Projectile.damage, 1f, base.Projectile.owner);
			}
		}
	}

	private void SpawnDust()
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		Vector2 dspeed = default(Vector2);
		for (int i = 0; i < 50; i++)
		{
			int dusttype = (Main.rand.NextBool() ? 68 : 67);
			if (Main.rand.NextBool(4))
			{
				dusttype = 80;
			}
			((Vector2)(ref dspeed))._002Ector(Main.rand.NextFloat(-7f, 7f), Main.rand.NextFloat(-7f, 7f));
			int dust = Dust.NewDust(base.Projectile.Center, 1, 1, dusttype, dspeed.X, dspeed.Y, 50, default(Color), 1.1f);
			Main.dust[dust].noGravity = true;
		}
	}

	private void SpawnFlames()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item34, base.Projectile.Center);
		for (int i = 0; i < 360; i += 60)
		{
			Vector2 pspeed1 = Utils.RotatedBy(new Vector2(0.9f, 0.9f), (double)MathHelper.ToRadians((float)(i + 20) + MathHelper.ToDegrees(base.Projectile.rotation)), default(Vector2));
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, pspeed1, ModContent.ProjectileType<EndoFire>(), base.Projectile.damage, 1f, base.Projectile.owner);
		}
	}
}
