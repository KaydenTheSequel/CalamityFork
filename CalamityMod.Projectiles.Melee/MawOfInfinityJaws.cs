using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class MawOfInfinityJaws : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	private Vector2 GoalPos;

	private Vector2 StartPos;

	private float offset;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Particles/Jaws";

	public override void SetDefaults()
	{
		base.Projectile.width = 500;
		base.Projectile.height = 500;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 35;
		base.Projectile.alpha = 100;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		writer.WriteVector2(GoalPos);
		writer.WriteVector2(StartPos);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		GoalPos = reader.ReadVector2();
		StartPos = reader.ReadVector2();
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (GoalPos == Vector2.Zero)
		{
			GoalPos = player.Calamity().mouseWorld;
			StartPos = base.Projectile.Center;
			base.Projectile.netUpdate = true;
		}
		base.Projectile.Center = Vector2.Lerp(StartPos, GoalPos, MathHelper.Min(1f, MathF.Pow(1f - (float)(base.Projectile.timeLeft - 5) / 30f, 0.5f)));
		base.Projectile.rotation = StartPos.DirectionTo(GoalPos).ToRotation();
		offset = 200f * MathHelper.Min(MathF.Pow(1f - (float)(base.Projectile.timeLeft - 5) / 30f, 0.4f), (float)(base.Projectile.timeLeft - 5) / 5f);
		if (offset < 16f)
		{
			offset = 16f;
		}
		if (base.Projectile.timeLeft == 5)
		{
			base.Projectile.friendly = true;
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCKilled/DevourerSegmentBreak1");
			style.Volume = 0.3f;
			style.PitchVariance = 0.3f;
			SoundEngine.PlaySound(in style, base.Projectile.position);
			style = SoundID.Item62 with
			{
				Volume = 0.5f,
				PitchVariance = 0.3f
			};
			SoundEngine.PlaySound(in style, base.Projectile.position);
			for (int i = 0; i < 35; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 181, Utils.RotatedByRandom(new Vector2(4.5f, 4.5f), 100.0) * Main.rand.NextFloat(0.2f, 1.9f), 0, default(Color), Main.rand.NextFloat(1.5f, 2.8f));
				dust.shader = GameShaders.Armor.GetSecondaryShader(player.cShield, player);
				dust.noGravity = true;
			}
			for (int j = 0; j < 14; j++)
			{
				Vector2 dustVel = Utils.RotatedByRandom(new Vector2(6f, 6f), 100.0) * Main.rand.NextFloat(0.5f, 1.2f);
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + dustVel * 2f, 272, dustVel);
				dust2.shader = GameShaders.Armor.GetSecondaryShader(player.cShield, player);
				Dust.NewDustPerfect(base.Projectile.Center + dustVel * 2f, 226, dustVel);
				dust2.shader = GameShaders.Armor.GetSecondaryShader(player.cShield, player);
			}
			if (Main.LocalPlayer.Distance(base.Projectile.Center) < 1600f)
			{
				Main.LocalPlayer.SetScreenshake(5f);
			}
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, Color.Aqua, new Vector2(2f, 2f), 0f, 0.2f, 1.7f, 36));
			GeneralParticleHandler.SpawnParticle(new DetailedExplosion(base.Projectile.Center, Vector2.Zero, Color.Magenta, Vector2.One, Main.rand.NextFloat(-5f, 5f), 0f, 1.3f, 26));
		}
		time++;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		base.Projectile.tileCollide = false;
		if (base.Projectile.timeLeft > 85)
		{
			base.Projectile.timeLeft = 85;
		}
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft < 85)
		{
			byte b2 = (byte)(base.Projectile.timeLeft * 3);
			byte a2 = (byte)(100f * ((float)(int)b2 / 255f));
			return new Color((int)b2, (int)b2, (int)b2, (int)a2);
		}
		return default(Color);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		float jawScaleMult = 1f + 0.007f * (float)time;
		jawScaleMult = MathF.Pow(jawScaleMult, 3f);
		float rotationOff = 0.5f * MathHelper.Min(MathF.Pow(MathHelper.Clamp(1f - (float)(base.Projectile.timeLeft - 5) / 30f, 0f, 1f), 0.5f), MathF.Pow(MathHelper.Clamp((float)(base.Projectile.timeLeft - 5) / 10f, 0f, 1f), 0.5f));
		if (rotationOff < 0.01f)
		{
			rotationOff = 0.01f;
		}
		float drawRot = base.Projectile.rotation;
		Main.spriteBatch.SetBlendState(BlendState.Additive);
		Main.spriteBatch.Draw(tex, base.Projectile.Center + Utils.RotatedBy(new Vector2(0f, offset), (double)drawRot, default(Vector2)) - Main.screenPosition, (Rectangle?)tex.Frame(2), Color.Fuchsia, base.Projectile.rotation + rotationOff + (float)Math.PI / 2f, new Vector2((float)tex.Width * 0.25f, (float)tex.Height * 0.5f), jawScaleMult, (SpriteEffects)(base.Projectile.spriteDirection != -1), 0f);
		Main.spriteBatch.Draw(tex, base.Projectile.Center + Utils.RotatedBy(new Vector2(0f, 0f - offset), (double)drawRot, default(Vector2)) - Main.screenPosition, (Rectangle?)tex.Frame(2, 1, 1), Color.Aqua, base.Projectile.rotation - rotationOff + (float)Math.PI / 2f, new Vector2((float)tex.Width * 0.25f, (float)tex.Height * 0.5f), jawScaleMult, (SpriteEffects)(base.Projectile.spriteDirection != -1), 0f);
		Main.spriteBatch.SetBlendState(BlendState.AlphaBlend);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 360);
	}

	public MawOfInfinityJaws()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		GoalPos = Vector2.Zero;
		StartPos = Vector2.Zero;
		base._002Ector();
	}
}
