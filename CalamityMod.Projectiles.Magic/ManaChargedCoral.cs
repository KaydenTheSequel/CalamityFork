using System;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class ManaChargedCoral : ModProjectile, ILocalizedModType, IModType
{
	private Vector2 offsetFromStuckNPC;

	private float rotationFromStuckNPC;

	public new string LocalizationCategory => "Projectiles.Magic";

	public Player Owner => Main.player[base.Projectile.owner];

	public static float FullMana => 180f;

	public ref float ManaCharge => ref base.Projectile.ai[0];

	public NPC StuckNPC
	{
		get
		{
			if (base.Projectile.numHits <= 0)
			{
				return null;
			}
			return Main.npc[(int)base.Projectile.ai[1]];
		}
	}

	public ref float DetachmentEffectsComplete => ref base.Projectile.localAI[0];

	public bool Stuck
	{
		get
		{
			if (StuckNPC != null && StuckNPC.active)
			{
				return ManaCharge < FullMana;
			}
			return false;
		}
	}

	public bool HasStuck => base.Projectile.numHits > 0;

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 3;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 360;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override bool ShouldUpdatePosition()
	{
		return !Stuck;
	}

	public override bool? CanDamage()
	{
		return base.Projectile.numHits == 0;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (HasStuck)
		{
			base.Projectile.velocity.X *= 0.86f;
			return false;
		}
		return true;
	}

	public override void AI()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0519: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0602: Unknown result type (might be due to invalid IL or missing references)
		//IL_0608: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_062c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0637: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_0655: Unknown result type (might be due to invalid IL or missing references)
		//IL_065a: Unknown result type (might be due to invalid IL or missing references)
		//IL_065f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0669: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.frame == 0)
		{
			base.Projectile.frame = Main.rand.Next(3) + 1;
		}
		Vector2 center2;
		if (Stuck)
		{
			Projectile projectile = base.Projectile;
			Vector2 center = StuckNPC.Center;
			Vector2 spinningpoint = offsetFromStuckNPC;
			double radians = StuckNPC.rotation;
			center2 = default(Vector2);
			projectile.Center = center + spinningpoint.RotatedBy(radians, center2) * StuckNPC.scale;
			base.Projectile.rotation = StuckNPC.rotation + rotationFromStuckNPC;
			base.Projectile.tileCollide = false;
			base.Projectile.timeLeft++;
			ManaCharge++;
			if (Main.rand.NextBool(5))
			{
				GeneralParticleHandler.SpawnParticle(new CuteManaStarParticle(base.Projectile.Center + Main.rand.NextVector2Circular(19f, 19f), Main.rand.NextVector2Circular(4f, 4f) - Vector2.UnitY * 6f * Main.rand.NextFloat(0.6f, 1.2f), Main.rand.NextFloat(0.8f, 1.8f), 1f, Main.rand.Next(14) + 14));
			}
		}
		else if (HasStuck)
		{
			if (DetachmentEffectsComplete != 1f)
			{
				base.Projectile.tileCollide = true;
				base.Projectile.timeLeft = 360;
				DetachmentEffectsComplete = 1f;
				base.Projectile.velocity = Vector2.UnitY.RotatedByRandom(1.5707963705062866) * -1f * (Main.rand.NextFloat(5f) + 5f);
			}
			if (base.Projectile.velocity.X != 0f)
			{
				base.Projectile.rotation += 0.02f * (float)Math.Sign(base.Projectile.velocity.X) * Math.Clamp(((Vector2)(ref base.Projectile.velocity)).Length(), 0f, 5f);
			}
			base.Projectile.velocity.X *= 0.987f;
			base.Projectile.velocity.Y += 0.8f;
			center2 = Owner.Center - base.Projectile.Center;
			float distanceToOwner = ((Vector2)(ref center2)).Length();
			if (distanceToOwner < 170f)
			{
				base.Projectile.timeLeft++;
				base.Projectile.velocity = (Owner.Center - base.Projectile.Center).SafeNormalize(Vector2.Zero) * Math.Max(distanceToOwner * 0.09f, 8f);
				if (distanceToOwner < 10f)
				{
					base.Projectile.Kill();
				}
			}
			if (Main.rand.NextBool())
			{
				Vector2 position = base.Projectile.Center + Main.rand.NextVector2Circular(15f, 15f);
				Vector2? velocity = Vector2.UnitY * -7f;
				int alpha = Main.rand.Next(100) + 120;
				float scale = Main.rand.NextFloat(1f, 2f);
				Dust.NewDustPerfect(position, 45, velocity, alpha, default(Color), scale).noGravity = true;
			}
			if (Main.rand.NextBool(5))
			{
				GeneralParticleHandler.SpawnParticle(new CuteManaStarParticle(base.Projectile.Center + Main.rand.NextVector2Circular(19f, 19f), -Vector2.UnitY * 6f * Main.rand.NextFloat(0.6f, 1.2f), Main.rand.NextFloat(0.8f, 1.8f), 1f, Main.rand.Next(14) + 14));
			}
		}
		else
		{
			float fallSpeed = base.Projectile.velocity.Y;
			if (base.Projectile.velocity.X != 0f)
			{
				base.Projectile.rotation += 0.02f * (float)Math.Sign(base.Projectile.velocity.X) * Math.Clamp(((Vector2)(ref base.Projectile.velocity)).Length(), 0f, 5f);
			}
			if (base.Projectile.timeLeft < 345)
			{
				Projectile projectile2 = base.Projectile;
				projectile2.velocity += Vector2.UnitY * 0.5f * (1f - Math.Clamp(((float)base.Projectile.timeLeft - 310f) / 35f, 0f, 1f));
			}
			Projectile projectile3 = base.Projectile;
			projectile3.velocity *= 0.98f;
			if (base.Projectile.velocity.Y > 0f)
			{
				base.Projectile.velocity.Y = Math.Clamp(base.Projectile.velocity.Y, 0f, Math.Max(18f, fallSpeed));
			}
			if (Main.rand.NextBool())
			{
				Vector2 position2 = base.Projectile.Center + Main.rand.NextVector2Circular(15f, 15f);
				Vector2? velocity2 = base.Projectile.velocity * 0.3f;
				int alpha2 = Main.rand.Next(100) + 120;
				float scale = Main.rand.NextFloat(1f, 2f);
				Dust.NewDustPerfect(position2, 15, velocity2, alpha2, default(Color), scale).noGravity = true;
			}
			if (Main.rand.NextBool(3))
			{
				GeneralParticleHandler.SpawnParticle(new CuteManaStarParticle(base.Projectile.Center, base.Projectile.velocity * 0.2f + Main.rand.NextVector2Circular(5f, 5f) - Vector2.UnitY * 3f, Main.rand.NextFloat(0.8f, 1.8f), 1f, Main.rand.Next(14) + 14));
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.GlommerBounce, base.Projectile.Center);
		base.Projectile.velocity = Vector2.Zero;
		base.Projectile.ai[1] = target.whoAmI;
		offsetFromStuckNPC = (base.Projectile.Center - target.Center).RotatedBy(0f - target.rotation) / target.scale;
		rotationFromStuckNPC = base.Projectile.rotation - target.rotation;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = Owner.Center - base.Projectile.Center;
		if (((Vector2)(ref val)).Length() < 10f && HasStuck)
		{
			SoundStyle style = SoundID.Item28 with
			{
				Volume = SoundID.Item28.Volume * 0.8f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			int manaGained = Math.Max(10, (int)Math.Floor(150f * Math.Clamp(ManaCharge * 2f / FullMana, 0f, 1f)));
			Owner.statMana += manaGained;
			if (Main.myPlayer == Owner.whoAmI)
			{
				Owner.ManaEffect(manaGained);
			}
			if (Owner.statMana > Owner.statManaMax2)
			{
				Owner.statMana = Owner.statManaMax2;
			}
			Owner.AddBuff(ModContent.BuffType<CoralSymbiosis>(), CoralSpout.SymbiosisTime);
		}
		else if (timeLeft != 0)
		{
			SoundEngine.PlaySound(in SoundID.Item171, base.Projectile.Center);
			for (int i = 0; i < 8; i++)
			{
				float angle = Main.rand.NextFloat((float)Math.PI * 2f);
				GeneralParticleHandler.SpawnParticle(new UrchinSpikeParticle(base.Projectile.Center + angle.ToRotationVector2() * 2f, angle.ToRotationVector2() * 6f, angle + (float)Math.PI / 2f, Main.rand.NextFloat(1f, 1.3f), 1f, Main.rand.Next(10) + 25));
			}
			int dustCount = Main.rand.Next(7);
			for (int j = 0; j < dustCount; j++)
			{
				int dustOpacity = (int)(200f * Main.rand.NextFloat(0.5f, 1f));
				float dustScale = Main.rand.NextFloat(1f, 1.4f);
				int dustType = CoralSpike.DustPick;
				Vector2 dustVelocity = (float)((dustType == 255) ? 1 : (-1)) * base.Projectile.velocity.RotatedByRandom(0.7853981852531433) * 0.6f + Main.rand.NextVector2Circular(7f, 7f);
				Vector2 center = base.Projectile.Center;
				Vector2? velocity = dustVelocity;
				float scale = dustScale;
				Dust.NewDustPerfect(center, dustType, velocity, dustOpacity, default(Color), scale).noGravity = true;
			}
			int goreNumber = Main.rand.Next(4);
			for (int k = 0; k < goreNumber; k++)
			{
				int goreID = (Main.rand.NextBool() ? 266 : (Main.rand.NextBool() ? 971 : 972));
				Gore gore = Gore.NewGorePerfect(base.Projectile.GetSource_FromAI(), base.Projectile.position, base.Projectile.velocity * -0.2f + Main.rand.NextVector2Circular(5f, 5f), goreID);
				gore.scale = Main.rand.NextFloat(0.6f, 1f) * ((goreID == 972) ? 0.7f : 1f);
				gore.type = goreID;
			}
		}
		else if (HasStuck)
		{
			for (int l = 0; l < 14; l++)
			{
				Vector2 direction = Main.rand.NextVector2Circular(10f, 10f);
				Vector2 position = base.Projectile.Center + direction;
				Vector2? velocity2 = direction + Vector2.UnitY * -7f;
				int alpha = Main.rand.Next(100) + 120;
				float scale = Main.rand.NextFloat(1f, 2f);
				Dust.NewDustPerfect(position, 45, velocity2, alpha, default(Color), scale).noGravity = true;
			}
			SoundStyle style = SoundID.Item29 with
			{
				Volume = SoundID.Item29.Volume * 0.4f,
				Pitch = -0.3f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 position = base.Projectile.Center - Main.screenPosition;
		int variant = base.Projectile.frame - 1;
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(0, 26 * variant, 24, 24);
		float scale = 1.3f;
		Main.EntitySpriteDraw(texture, position, frame, lightColor, base.Projectile.rotation, frame.Size() / 2f, scale, (SpriteEffects)0);
		if (Stuck || HasStuck)
		{
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			Texture2D bloomTex = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
			float bloomSize = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 3f) * 0.1f + 0.4f;
			float period = (int)(FullMana / 4f);
			float pulseProgress = Main.GlobalTimeWrappedHourly % period / period;
			if (Stuck)
			{
				pulseProgress = ManaCharge % period / period;
			}
			float overimageSize = scale + (float)Math.Sqrt(pulseProgress) * 0.4f;
			float overimageOpacity = (float)Math.Sin(pulseProgress * ((float)Math.PI / 2f) + (float)Math.PI / 2f) * 0.7f;
			Main.EntitySpriteDraw(bloomTex, position, null, Color.DodgerBlue * 0.3f, 0f, bloomTex.Size() / 2f, bloomSize, (SpriteEffects)0);
			Main.EntitySpriteDraw(texture, position, frame, Color.DodgerBlue * overimageOpacity, base.Projectile.rotation, frame.Size() / 2f, overimageSize, (SpriteEffects)0);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		}
		return false;
	}
}
