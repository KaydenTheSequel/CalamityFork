using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class DormantBrimseekerBab : ModProjectile, ILocalizedModType, IModType
{
	public const float DistanceToCheck = 1600f;

	public const int TurnTime = 12;

	public bool SeekingTarget;

	public new string LocalizationCategory => "Projectiles.Summon";

	public float MaxChargeTime => (base.Projectile.localAI[1] == 1f) ? 16 : 25;

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 8;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 7;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 46;
		base.Projectile.height = 36;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.minion = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0608: Unknown result type (might be due to invalid IL or missing references)
		//IL_060d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0612: Unknown result type (might be due to invalid IL or missing references)
		//IL_0616: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		bool num = base.Projectile.type == ModContent.ProjectileType<DormantBrimseekerBab>();
		player.AddBuff(ModContent.BuffType<BrimseekerBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				modPlayer.brimseeker = false;
			}
			if (modPlayer.brimseeker)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		NPC potentialTarget = base.Projectile.Center.MinionHoming(1600f, player);
		if (potentialTarget == null)
		{
			if (base.Projectile.ai[0] != 0f)
			{
				base.Projectile.ai[0] = 0f;
			}
			base.Projectile.rotation = base.Projectile.rotation.AngleTowards(0f, 0.2f);
			base.Projectile.direction = (base.Projectile.spriteDirection = (base.Projectile.velocity.X < 0f).ToDirectionInt());
			base.Projectile.ai[1] += MathHelper.ToRadians(3f);
			if (base.Projectile.ai[1] >= (float)Math.PI * 4f)
			{
				base.Projectile.ai[1] = 0f;
			}
			Vector2 destination = player.Center + base.Projectile.ai[1].ToRotationVector2() * new Vector2(1f, (float)Math.Cos(base.Projectile.ai[1])) * 200f;
			base.Projectile.velocity = (base.Projectile.velocity * 18f + base.Projectile.SafeDirectionTo(destination) * 14f) / 20f;
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter >= 6)
			{
				base.Projectile.frameCounter = 0;
				base.Projectile.frame++;
			}
			if (base.Projectile.frame >= 4 + (base.Projectile.localAI[1] == 1f).ToInt() * 4)
			{
				base.Projectile.frame = (base.Projectile.localAI[1] == 1f).ToInt() * 4;
			}
			base.Projectile.MinionAntiClump(0.1f);
			if (base.Projectile.Distance(player.Center) > 2700f)
			{
				base.Projectile.Center = player.Center;
				base.Projectile.netUpdate = true;
			}
		}
		else
		{
			if (base.Projectile.Distance(potentialTarget.Center) < 400f && base.Projectile.ai[0] == 0f)
			{
				SoundEngine.PlaySound(in SoundID.DD2_DrakinShot, base.Projectile.Center);
				base.Projectile.ai[0]++;
				float acceleration = ((base.Projectile.localAI[1] == 1f) ? 1.5f : 1.1f);
				float minSpeed = ((base.Projectile.localAI[1] == 1f) ? 17.5f : 15f);
				float maxSpeed = ((base.Projectile.localAI[1] == 1f) ? 28f : 24f);
				base.Projectile.velocity = base.Projectile.SafeDirectionTo(potentialTarget.Center) * MathHelper.Clamp(((Vector2)(ref base.Projectile.velocity)).Length() + acceleration, minSpeed, maxSpeed);
				base.Projectile.rotation = base.Projectile.AngleTo(potentialTarget.Center) + (float)(base.Projectile.spriteDirection == 1).ToInt() * (float)Math.PI;
			}
			else if (base.Projectile.ai[0] > 0f)
			{
				base.Projectile.ai[0]++;
				SeekingTarget = false;
			}
			else if (base.Projectile.Distance(potentialTarget.Center) >= 400f)
			{
				base.Projectile.rotation = base.Projectile.rotation.AngleTowards(base.Projectile.AngleTo(potentialTarget.Center) + (float)(base.Projectile.spriteDirection == 1).ToInt() * (float)Math.PI, 0.3f);
				base.Projectile.velocity = base.Projectile.SafeDirectionTo(potentialTarget.Center) * MathHelper.Clamp(((Vector2)(ref base.Projectile.velocity)).Length() + 2f, 10f, 32f);
				SeekingTarget = true;
				base.Projectile.ai[0] = 0f;
			}
			if (base.Projectile.ai[0] >= MaxChargeTime)
			{
				base.Projectile.rotation = base.Projectile.rotation.AngleTowards(base.Projectile.AngleTo(potentialTarget.Center) + (float)(base.Projectile.spriteDirection == 1).ToInt() * (float)Math.PI, 0.3f);
			}
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter >= 6)
			{
				base.Projectile.frameCounter = 0;
				base.Projectile.frame++;
			}
			if (base.Projectile.frame >= 8)
			{
				base.Projectile.frame = 4;
			}
			if (base.Projectile.ai[0] >= MaxChargeTime + 12f)
			{
				base.Projectile.ai[0] = 0f;
			}
		}
		Vector2 center = base.Projectile.Center;
		Color red = Color.Red;
		Lighting.AddLight(center, ((Color)(ref red)).ToVector3());
	}

	public override bool MinionContactDamage()
	{
		return true;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		if ((!(base.Projectile.ai[0] > 0f) || !(base.Projectile.ai[0] <= MaxChargeTime) || !(((Vector2)(ref base.Projectile.velocity)).Length() >= 8f)) && !SeekingTarget)
		{
			return;
		}
		Texture2D projectileTexture = TextureAssets.Projectile[base.Type].Value;
		Texture2D flameTexture = TextureAssets.Extra[55].Value;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		float completionRatio = base.Projectile.ai[0] / 30f;
		if (base.Projectile.ai[0] > 30f)
		{
			completionRatio = 1f - (30f - base.Projectile.ai[0]) / 30f;
		}
		Vector2 modifiedProjectileTexture = default(Vector2);
		((Vector2)(ref modifiedProjectileTexture))._002Ector((float)(projectileTexture.Width / 2), (float)(projectileTexture.Height / Main.projFrames[base.Type] / 2));
		Vector2 flameOrigin = default(Vector2);
		for (int oldPositionDrawIndex = 6; oldPositionDrawIndex >= 0; oldPositionDrawIndex--)
		{
			Color drawColor = Color.Lerp(Color.LightGoldenrodYellow, new Color(142, 24, 67), completionRatio);
			drawColor = Color.Lerp(drawColor, new Color(142, 24, 67), (float)oldPositionDrawIndex / 9.7f);
			((Color)(ref drawColor)).A = (byte)(64f * completionRatio);
			((Color)(ref drawColor)).R = (byte)(((Color)(ref drawColor)).R * (10 - oldPositionDrawIndex) / 20);
			((Color)(ref drawColor)).G = (byte)(((Color)(ref drawColor)).G * (10 - oldPositionDrawIndex) / 20);
			((Color)(ref drawColor)).B = (byte)(((Color)(ref drawColor)).B * (10 - oldPositionDrawIndex) / 20);
			((Color)(ref drawColor)).A = (byte)(((Color)(ref drawColor)).A * (10 - oldPositionDrawIndex) / 20);
			drawColor *= completionRatio;
			int yFrame = ((int)base.Projectile.ai[0] / 2 - oldPositionDrawIndex) % 4;
			if (yFrame < 0)
			{
				yFrame += 4;
			}
			Rectangle flameFrameRectangle = flameTexture.Frame(1, 4, 0, yFrame);
			((Vector2)(ref flameOrigin))._002Ector((float)(flameTexture.Width / 2), (float)(flameTexture.Height / 8 + 14));
			Main.EntitySpriteDraw(flameTexture, new Vector2(base.Projectile.oldPos[oldPositionDrawIndex].X - Main.screenPosition.X + (float)(base.Projectile.width / 2) - (float)projectileTexture.Width * base.Projectile.scale / 2f + modifiedProjectileTexture.X * base.Projectile.scale, base.Projectile.oldPos[oldPositionDrawIndex].Y - Main.screenPosition.Y + (float)base.Projectile.height - (float)projectileTexture.Height * base.Projectile.scale / (float)Main.projFrames[base.Type] + 4f + modifiedProjectileTexture.Y * base.Projectile.scale + base.Projectile.gfxOffY), flameFrameRectangle, drawColor, base.Projectile.oldRot[oldPositionDrawIndex] + (float)base.Projectile.oldSpriteDirection[oldPositionDrawIndex] * ((float)Math.PI / 2f), flameOrigin, MathHelper.Lerp(0.1f, 1.2f, (10f - (float)oldPositionDrawIndex) / 10f), spriteEffects);
		}
	}
}
