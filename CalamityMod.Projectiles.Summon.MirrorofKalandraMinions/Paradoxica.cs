using System;
using System.IO;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Particles;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon.MirrorofKalandraMinions;

public class Paradoxica : ModProjectile, ILocalizedModType, IModType
{
	public bool hasTeleported;

	public Vector2 ChargeStartingPosition;

	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public CalamityPlayer ModdedOwner => Owner.Calamity();

	public NPC Target
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Center.MinionHoming(MirrorofKalandra.TargetDistanceDetection, Owner);
		}
	}

	public ref float AITimer => ref base.Projectile.ai[0];

	public ref float Oscillation => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 12000;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.minionSlots = 1f;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = MirrorofKalandra.Scimitar_IFrames;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.width = (base.Projectile.height = 104);
		base.Projectile.ignoreWater = true;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.minion = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(hasTeleported);
		writer.WriteVector2(ChargeStartingPosition);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		hasTeleported = reader.ReadBoolean();
		ChargeStartingPosition = reader.ReadVector2();
	}

	public override void AI()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		CheckMinionExistence();
		if (Main.rand.NextBool(30))
		{
			GeneralParticleHandler.SpawnParticle(new GenericSparkle(base.Projectile.Center + Main.rand.NextVector2Circular(base.Projectile.width / 4, base.Projectile.height / 4), Vector2.Zero, Color.Lerp(Color.White, Color.Gold, Main.rand.NextFloat(1f)), Color.White, Main.rand.NextFloat(0.6f, 1f), 20, Main.rand.NextFloat(0.08f, 0.12f), Main.rand.NextFloat(0.1f, 0.3f)));
		}
		if (Target != null)
		{
			if (!hasTeleported)
			{
				int dustAmount = 60;
				for (int dustIndex = 0; dustIndex < dustAmount; dustIndex++)
				{
					Vector2 velocity = ((float)Math.PI * 2f / (float)dustAmount * (float)dustIndex).ToRotationVector2() * Main.rand.NextFloat(5f, 15f);
					Dust.NewDustPerfect(base.Projectile.Center, 212, velocity, 0, default(Color), 2f).noGravity = true;
				}
				base.Projectile.Center = Target.Center;
				hasTeleported = true;
			}
			int attackCycleTime = 66;
			float upwardRiseTimeRatio = 0.4f;
			float pierceTimeRatio = 0.14f;
			int num = (int)AITimer;
			if ((float)(num % attackCycleTime) == 1f)
			{
				ChargeStartingPosition = base.Projectile.Center + Main.rand.NextVector2Circular(80f, 80f);
				base.Projectile.netUpdate = true;
			}
			float attackCompletion = (float)num / (float)attackCycleTime % 1f;
			if (attackCompletion < upwardRiseTimeRatio)
			{
				base.Projectile.oldPos = (Vector2[])(object)new Vector2[base.Projectile.oldPos.Length];
			}
			base.Projectile.MaxUpdates = 2;
			float offsetDistanceFactor = MathHelper.Lerp(1.61f, 3f, 1f / 7f);
			Vector2 startingPosition = ChargeStartingPosition + Vector2.UnitY * Utils.GetLerpValue(0f, upwardRiseTimeRatio, attackCompletion, clamped: true) * -200f;
			Vector2 targetOffset = Target.Center - startingPosition;
			Vector2 endingPosition = Target.Center + targetOffset.SafeNormalize(Vector2.Zero) * MathHelper.Clamp(((Vector2)(ref targetOffset)).Length(), 60f, 240f) * offsetDistanceFactor;
			float pierceCompletion = Utils.GetLerpValue(upwardRiseTimeRatio, upwardRiseTimeRatio + pierceTimeRatio, attackCompletion, clamped: true);
			float throughTargetCompletion = Utils.GetLerpValue(upwardRiseTimeRatio + pierceTimeRatio, 1f, attackCompletion, clamped: true);
			base.Projectile.rotation = base.Projectile.rotation.AngleTowards(targetOffset.ToRotation() + (float)Math.PI / 2f, (float)Math.PI / 5f);
			base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, Vector2.Lerp(startingPosition, Target.Center, pierceCompletion), pierceCompletion * 0.5f);
			if (throughTargetCompletion > 0f)
			{
				base.Projectile.Center = Vector2.Lerp(Target.Center, endingPosition, throughTargetCompletion);
			}
			base.Projectile.velocity = Vector2.Zero;
			if (num % attackCycleTime == (int)((float)attackCycleTime * upwardRiseTimeRatio))
			{
				SoundStyle style = CommonCalamitySounds.MeatySlashSound with
				{
					Pitch = 1.6f,
					Volume = 0.1f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			AITimer++;
		}
		else if (Target == null)
		{
			base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, Owner.Center + base.Projectile.rotation.ToRotationVector2() * (MirrorofKalandra.IdleDistanceFromPlayer + MirrorofKalandra.IdleDistanceFromPlayer * (MathF.Sin(Oscillation) / MirrorofKalandra.OscillationRange)), 0.4f);
			base.Projectile.velocity = Vector2.Zero;
			base.Projectile.rotation = base.Projectile.rotation.AngleLerp(-(float)Math.PI / 3f, 0.15f);
			Oscillation += MirrorofKalandra.OscillationSpeed;
			hasTeleported = false;
			base.Projectile.extraUpdates = 0;
			AITimer = 0f;
		}
	}

	public void CheckMinionExistence()
	{
		Owner.AddBuff(ModContent.BuffType<KalandraMirrorBuff>(), 3600);
		if (base.Projectile.type == ModContent.ProjectileType<Paradoxica>())
		{
			if (Owner.dead)
			{
				ModdedOwner.KalandraMirror = false;
			}
			if (ModdedOwner.KalandraMirror)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		GeneralParticleHandler.SpawnParticle(new SparkParticle(Vector2.Lerp(base.Projectile.Center, target.Center, 0.8f), (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2() * 0.01f, affectedByGravity: false, 20, Main.rand.NextFloat(1.2f, 1.8f), Color.White));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Rectangle frame = texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		float rotation = ((Target != null) ? base.Projectile.rotation : (base.Projectile.rotation + (float)Math.PI / 4f));
		if (CalamityClientConfig.Instance.Afterimages && Target != null)
		{
			for (int i = 0; i < base.Projectile.oldPos.Length; i++)
			{
				Color gray = Color.Gray;
				((Color)(ref gray)).A = 125;
				Color afterimageDrawColor = gray * base.Projectile.Opacity * (1f - (float)i / (float)base.Projectile.oldPos.Length);
				Vector2 afterimageDrawPosition = base.Projectile.oldPos[i] + base.Projectile.Size * 0.5f - Main.screenPosition;
				Main.EntitySpriteDraw(texture, afterimageDrawPosition, frame, afterimageDrawColor, rotation, origin, base.Projectile.scale, (SpriteEffects)0);
			}
		}
		Main.EntitySpriteDraw(texture, drawPosition, frame, base.Projectile.GetAlpha(lightColor), rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
