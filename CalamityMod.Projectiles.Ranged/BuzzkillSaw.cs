using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class BuzzkillSaw : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle TileCollideGFB = new SoundStyle("CalamityMod/Sounds/Custom/MetalPipeFalling");

	public static Asset<Texture2D> SmallSlash;

	public static Asset<Texture2D> LargeSlash;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public ref float SawLevel => ref base.Projectile.ai[0];

	public ref float Time => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 40);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.timeLeft = 480;
		base.Projectile.penetrate = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		Time++;
		base.Projectile.rotation += MathHelper.ToRadians(6f + 18f * SawLevel);
		if (base.Projectile.frame < 1)
		{
			base.Projectile.frame = 1;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= 3)
		{
			base.Projectile.frameCounter = 0;
			base.Projectile.frame++;
			if (base.Projectile.frame > 3)
			{
				base.Projectile.frame = 1;
			}
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		int sparkCount = 6 + 5 * (int)SawLevel;
		for (int s = 0; s < sparkCount; s++)
		{
			Vector2 sparkVelocity = default(Vector2);
			if (base.Projectile.velocity.X != oldVelocity.X && oldVelocity.X < 0f)
			{
				sparkVelocity = Vector2.UnitX * 6.5f;
			}
			else if (base.Projectile.velocity.X != oldVelocity.X && oldVelocity.X >= 0f)
			{
				sparkVelocity = Vector2.UnitX * -6.5f;
			}
			else if (base.Projectile.velocity.Y != oldVelocity.Y && oldVelocity.Y < 0f)
			{
				sparkVelocity = Vector2.UnitY * 6.5f;
			}
			else if (base.Projectile.velocity.Y != oldVelocity.Y && oldVelocity.Y >= 0f)
			{
				sparkVelocity = Vector2.UnitY * -6.5f;
			}
			Vector2 relativePosition = ((sparkVelocity.X > 0f) ? base.Projectile.Left : ((sparkVelocity.X < 0f) ? base.Projectile.Right : ((sparkVelocity.Y > 0f) ? base.Projectile.Top : base.Projectile.Bottom)));
			sparkVelocity = sparkVelocity.RotatedByRandom(1.5707963705062866) * (Main.rand.NextFloat(0.8f, 1.2f) + Main.rand.NextFloat(0.2f, 0.6f) * SawLevel);
			float scale = Main.rand.NextFloat(0.5f, 0.8f) + Main.rand.NextFloat(0.2f, 0.6f) * SawLevel;
			GeneralParticleHandler.SpawnParticle(new AltLineParticle(relativePosition, sparkVelocity, affectedByGravity: false, 30, scale, new Color(250, 250, 107)));
		}
		base.Projectile.penetrate--;
		base.Projectile.numHits++;
		if (base.Projectile.penetrate <= 0)
		{
			base.Projectile.Kill();
		}
		else
		{
			SoundEngine.PlaySound(Main.zenithWorld ? TileCollideGFB : SoundID.Item178 with
			{
				Pitch = 0.15f * (float)base.Projectile.numHits
			}, base.Projectile.Center);
			if (base.Projectile.velocity.X != oldVelocity.X)
			{
				base.Projectile.velocity.X = 0f - oldVelocity.X;
			}
			if (base.Projectile.velocity.Y != oldVelocity.Y)
			{
				base.Projectile.velocity.Y = 0f - oldVelocity.Y;
			}
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 150);
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/SwiftSlice");
		style.Pitch = 0.15f * (float)base.Projectile.numHits;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		int bloodCount = 6 + 10 * (int)SawLevel;
		for (int p = 0; p < bloodCount; p++)
		{
			Vector2 velocity = base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(30f)) * (Main.rand.NextFloat(0.4f, 0.6f) + Main.rand.NextFloat(0.2f, 0.6f) * SawLevel);
			float scale = Main.rand.NextFloat(0.5f, 0.8f) + Main.rand.NextFloat(0.2f, 0.8f) * SawLevel;
			GeneralParticleHandler.SpawnParticle(new AltLineParticle(target.Center, velocity, affectedByGravity: false, 30, scale, new Color(112, 16, 16)));
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/CeramicImpact", 2), base.Projectile.Center);
		for (int d = 0; d < 8; d++)
		{
			Vector2 dustVel = Main.rand.NextVector2CircularEdge(1f, 1f);
			dustVel.SafeNormalize(Vector2.Zero);
			dustVel *= Main.rand.NextFloat(5f, 9f);
			Dust.NewDustPerfect(base.Projectile.Center, 84, dustVel).noGravity = true;
		}
		switch (Main.rand.Next(3))
		{
		case 0:
			Gore.NewGore(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Main.rand.NextVector2Circular(4f, 4f), base.Mod.Find<ModGore>("BuzzkillSaw2").Type, 0.8f);
			Gore.NewGore(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Main.rand.NextVector2Circular(4f, 4f), base.Mod.Find<ModGore>("BuzzkillSaw3").Type, 0.8f);
			break;
		case 1:
			Gore.NewGore(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Main.rand.NextVector2Circular(4f, 4f), base.Mod.Find<ModGore>("BuzzkillSaw1").Type, 0.8f);
			Gore.NewGore(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Main.rand.NextVector2Circular(4f, 4f), base.Mod.Find<ModGore>("BuzzkillSaw3").Type, 0.8f);
			break;
		case 2:
			Gore.NewGore(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Main.rand.NextVector2Circular(4f, 4f), base.Mod.Find<ModGore>("BuzzkillSaw1").Type, 0.8f);
			Gore.NewGore(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Main.rand.NextVector2Circular(4f, 4f), base.Mod.Find<ModGore>("BuzzkillSaw2").Type, 0.8f);
			break;
		}
	}

	public override void ModifyDamageHitbox(ref Rectangle hitbox)
	{
		if (SawLevel >= 2f)
		{
			((Rectangle)(ref hitbox)).Inflate(65, 65);
		}
		else if (SawLevel >= 1f)
		{
			((Rectangle)(ref hitbox)).Inflate(28, 28);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		if (LargeSlash == null)
		{
			LargeSlash = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/BuzzkillSawLargeSlash", (AssetRequestMode)2);
		}
		Texture2D largeSlashTexture = LargeSlash.Value;
		if (SmallSlash == null)
		{
			SmallSlash = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/BuzzkillSawSmallSlash", (AssetRequestMode)2);
		}
		Texture2D smallSlashTexture = SmallSlash.Value;
		Color slashColor = default(Color);
		((Color)(ref slashColor))._002Ector(200, 200, 200, 100);
		if (SawLevel >= 2f)
		{
			Main.EntitySpriteDraw(largeSlashTexture, base.Projectile.Center - Main.screenPosition, null, slashColor, 0f - base.Projectile.rotation, largeSlashTexture.Size() * 0.5f, 1f, (SpriteEffects)0);
			if (Time % 4f == 0f)
			{
				Vector2 randomParticleOffset = default(Vector2);
				((Vector2)(ref randomParticleOffset))._002Ector(Main.rand.NextFloat((float)(-base.Projectile.width) * 1.75f, (float)base.Projectile.width * 1.75f), Main.rand.NextFloat((float)(-base.Projectile.width) * 1.75f, (float)base.Projectile.width * 1.75f));
				float randomParticleScale = Main.rand.NextFloat(0.65f, 0.95f);
				GeneralParticleHandler.SpawnParticle(new BloomParticle(base.Projectile.Center + randomParticleOffset, base.Projectile.velocity, (Color)(Main.rand.NextBool() ? Color.White : new Color(112, 16, 16)), randomParticleScale, randomParticleScale, 4, fade: false));
			}
		}
		if (SawLevel >= 1f)
		{
			Main.EntitySpriteDraw(smallSlashTexture, base.Projectile.Center - Main.screenPosition, null, slashColor, base.Projectile.rotation, smallSlashTexture.Size() * 0.5f, 1f, (SpriteEffects)0);
			if (Time % 4f == 0f)
			{
				Vector2 randomParticleOffset2 = default(Vector2);
				((Vector2)(ref randomParticleOffset2))._002Ector(Main.rand.NextFloat(-base.Projectile.width, base.Projectile.width), Main.rand.NextFloat(-base.Projectile.width, base.Projectile.width));
				float randomParticleScale2 = Main.rand.NextFloat(0.35f, 0.65f);
				GeneralParticleHandler.SpawnParticle(new BloomParticle(base.Projectile.Center + randomParticleOffset2, base.Projectile.velocity, (Color)(Main.rand.NextBool() ? Color.White : new Color(112, 16, 16)), randomParticleScale2, randomParticleScale2, 4, fade: false));
			}
		}
		if (!CalamityClientConfig.Instance.Afterimages)
		{
			return true;
		}
		Texture2D buzzsawTexture = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = buzzsawTexture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		for (int i = 1; i < base.Projectile.oldPos.Length; i++)
		{
			float afterimageRot = base.Projectile.oldRot[i];
			Vector2 drawPos = base.Projectile.oldPos[i] + frame.Size() * 0.5f - Main.screenPosition;
			float intensity = MathHelper.Lerp(0.1f, 0.6f, 1f - (float)i / (float)base.Projectile.oldPos.Length);
			Main.EntitySpriteDraw(buzzsawTexture, drawPos, frame, lightColor * intensity, afterimageRot, frame.Size() * 0.5f, 1f, (SpriteEffects)0);
			if (SawLevel >= 2f)
			{
				Main.EntitySpriteDraw(largeSlashTexture, drawPos, null, slashColor * intensity, 0f - afterimageRot, largeSlashTexture.Size() * 0.5f, 1f, (SpriteEffects)0);
			}
			if (SawLevel >= 1f)
			{
				Main.EntitySpriteDraw(smallSlashTexture, drawPos, null, slashColor * intensity, afterimageRot, smallSlashTexture.Size() * 0.5f, 1f, (SpriteEffects)0);
			}
		}
		return true;
	}
}
