using System;
using System.IO;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using CalamityMod.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class GodSlayerSlugProj : ModProjectile, ILocalizedModType, IModType
{
	private const int Lifetime = 600;

	private const int NoDrawFrames = 2;

	private const int BlueNoCollideFrames = 25;

	private const int TurnBlueFrameDelay = 7;

	private const float MouseAimDeviation = 13f;

	private const int TextureHeight = 136;

	private static Texture2D TextureBlue;

	public new string LocalizationCategory => "Projectiles.Ranged";

	private bool BlueMode => base.Projectile.ai[0] != 0f;

	public override string Texture => "CalamityMod/Projectiles/Ranged/GodSlayerSlugPurple";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		if (!Main.dedServ)
		{
			TextureBlue = base.Mod.Assets.Request<Texture2D>("Projectiles/Ranged/GodSlayerSlugBlue", (AssetRequestMode)1).Value;
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
		base.Projectile.aiStyle = 1;
		base.AIType = 14;
		base.Projectile.alpha = 255;
		base.Projectile.MaxUpdates = 6;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.timeLeft = 600;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.tileCollide);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.tileCollide = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = ((Vector2)(ref base.Projectile.velocity)).Length();
		}
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 17;
		}
		if (BlueMode)
		{
			Lighting.AddLight(base.Projectile.Center, 0.06f, 0.24f, 0.29f);
		}
		else
		{
			Lighting.AddLight(base.Projectile.Center, 0.3f, 0.2f, 0.32f);
		}
		if (base.Projectile.numHits > 0 && base.Projectile.FinalExtraUpdate() && !BlueMode)
		{
			base.Projectile.ai[1]++;
		}
		if (!BlueMode && base.Projectile.ai[1] >= 7f)
		{
			TurnBlue(setPosition: true);
		}
		if (BlueMode && base.Projectile.ai[1] > 0f)
		{
			base.Projectile.ai[1]--;
			if (base.Projectile.ai[1] == 0f)
			{
				base.Projectile.tileCollide = true;
			}
		}
	}

	private void TurnBlue(bool setPosition = false)
	{
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[0] = 1f;
		base.Projectile.ai[1] = 25f;
		base.Projectile.tileCollide = false;
		base.Projectile.damage = (int)(0.25f * (float)base.Projectile.damage);
		base.Projectile.penetrate = 1;
		for (int i = 0; i < Main.maxNPCs; i++)
		{
			base.Projectile.localNPCImmunity[i] = 0;
		}
		base.Projectile.timeLeft = 600 - 2 * base.Projectile.MaxUpdates;
		if (!setPosition || Main.myPlayer != base.Projectile.owner)
		{
			return;
		}
		ProduceWarpCrossDust(base.Projectile.Center, ModContent.DustType<SquashDust>(), 0.5f, Color.Magenta);
		base.Projectile.netUpdate = true;
		base.Projectile.tileCollide = false;
		Vector2 playerToMouseVec = Main.LocalPlayer.SafeDirectionTo(Main.MouseWorld, -Vector2.UnitY);
		float num = Main.rand.NextFloat(70f, 96f);
		float warpAngle = Main.rand.NextFloat(-(float)Math.PI / 3f, (float)Math.PI / 3f);
		Vector2 warpOffset = (0f - num) * playerToMouseVec.RotatedBy(warpAngle);
		base.Projectile.position = Main.LocalPlayer.MountedCenter + warpOffset;
		Vector2 mouseTargetVec = Main.MouseWorld + Main.rand.NextVector2Circular(13f, 13f);
		Vector2 bulletToMouseVec = base.Projectile.SafeDirectionTo(mouseTargetVec, -Vector2.UnitY);
		base.Projectile.velocity = bulletToMouseVec * base.Projectile.localAI[0];
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		for (int j = 0; j < ProjectileID.Sets.TrailCacheLength[base.Type]; j++)
		{
			if (!(base.Projectile.oldPos[j] == Vector2.Zero))
			{
				base.Projectile.oldPos[j] = base.Projectile.position;
			}
		}
		Vector2 warpInDustPos = base.Projectile.Center - bulletToMouseVec * 136f;
		ProduceWarpCrossDust(warpInDustPos, ModContent.DustType<SquashDust>(), 1f, Color.Cyan);
		for (int k = 0; k < 3; k++)
		{
			Vector2 dustVel = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedByRandom(0.05000000074505806) * Main.rand.NextFloat(9f, 15f);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<VoidDust>());
			dust.position = warpInDustPos;
			dust.velocity = dustVel;
			dust.noGravity = true;
			dust.scale *= Main.rand.NextFloat(0.6f, 1f);
			dust.color = Color.Cyan;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255, 255, 255, 140);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft >= 600 - 2 * base.Projectile.MaxUpdates)
		{
			return false;
		}
		CalamityUtils.DrawAfterimagesFromEdge(base.Projectile, 0, lightColor, BlueMode ? TextureBlue : null);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		if (!BlueMode)
		{
			TurnBlue();
		}
		base.Projectile.ExpandHitboxBy(48);
		base.Projectile.Damage();
		int dustID = ModContent.DustType<SquashDust>();
		int numDust = 9;
		float triangleAngle = Main.rand.NextFloat((float)Math.PI * 2f);
		for (int i = 0; i < numDust; i++)
		{
			float lerp = (float)i / (float)(numDust - 1);
			float speed = MathHelper.Lerp(0.2f, 3.6f, lerp);
			Vector2 dustVel = Vector2.UnitX.RotatedBy(triangleAngle) * speed * 2f;
			Dust dust = Dust.NewDustDirect(base.Projectile.Center, 0, 0, dustID);
			dust.position = base.Projectile.Center;
			dust.velocity = dustVel;
			dust.noGravity = true;
			dust.fadeIn = 1.5f;
			dust.scale *= Main.rand.NextFloat(1.4f, 1.9f) - lerp * 0.5f;
			dust.color = Color.Lerp(Color.Cyan, Color.Magenta, lerp);
			DustExtensions.BetterCloneDust(dust).velocity = dustVel.RotatedBy(2.094395160675049);
			DustExtensions.BetterCloneDust(dust).velocity = dustVel.RotatedBy(4.188790321350098);
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.numHits > 0 && !BlueMode)
		{
			TurnBlue(setPosition: true);
			return false;
		}
		Collision.HitTiles(base.Projectile.position, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		SoundEngine.PlaySound(in SoundID.Dig, base.Projectile.Center);
		return true;
	}

	private void ProduceWarpCrossDust(Vector2 dustPos, int dustID, float speedMultiplier, Color color)
	{
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		if (speedMultiplier > 0.8f)
		{
			for (int i = 0; i < 2; i++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(dustPos, Vector2.Zero, Color.Black, "CalamityMod/ExtraTextures/BasicCircle", Vector2.One, 0f, 0.4f * speedMultiplier, 0.05f * speedMultiplier, 12, UseAdditiveBlend: false, 1f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(dustPos, Vector2.Zero, color, "CalamityMod/Particles/BloomRing", Vector2.One, 0f, 0.15f * (1f + (float)i * 0.2f) * speedMultiplier, 0.025f * (1f + (float)i * 0.2f) * speedMultiplier, 12, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
		}
		else
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(dustPos, Vector2.Zero, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 10, 0.3f * speedMultiplier, color, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: true));
		}
		for (int j = 0; j < 5; j++)
		{
			float speed = Main.rand.NextFloat(3f, 6f);
			Vector2 dustVel = Vector2.UnitX * speed * speedMultiplier * 1.5f;
			Dust dust = Dust.NewDustDirect(base.Projectile.Center, 0, 0, dustID);
			dust.position = dustPos;
			dust.velocity = dustVel;
			dust.noGravity = true;
			dust.scale *= Main.rand.NextFloat(1.8f, 2.2f) * (1f - speed / 7f);
			dust.color = color;
			dust.fadeIn = 1f;
			DustExtensions.BetterCloneDust(dust).velocity = dustVel.RotatedBy(1.5707963705062866);
			DustExtensions.BetterCloneDust(dust).velocity = dustVel.RotatedBy(3.1415927410125732);
			DustExtensions.BetterCloneDust(dust).velocity = dustVel.RotatedBy(-1.5707963705062866);
		}
	}
}
