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

public class MidnightSunUFO : ModProjectile, ILocalizedModType, IModType
{
	public const float DistanceToCheck = 2600f;

	public new string LocalizationCategory => "Projectiles.Summon";

	public ref float Timer => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 26;
		base.Projectile.height = 58;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.minion = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0550: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0578: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0583: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_0598: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color skyBlue = Color.SkyBlue;
		Lighting.AddLight(center, ((Color)(ref skyBlue)).ToVector3());
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.velocity.Y = Main.rand.NextFloat(8f, 11f) * (float)Main.rand.NextBool().ToDirectionInt();
			base.Projectile.velocity.Y = Main.rand.NextFloat(3f, 5f) * (float)Main.rand.NextBool().ToDirectionInt();
			base.Projectile.localAI[0] = Main.rand.Next(1, 18);
		}
		bool num = base.Projectile.type == ModContent.ProjectileType<MidnightSunUFO>();
		player.AddBuff(ModContent.BuffType<MidnightSunBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				modPlayer.midnightUFO = false;
			}
			if (modPlayer.midnightUFO)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		NPC potentialTarget = base.Projectile.Center.MinionHoming(2600f, player);
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		if (potentialTarget != null)
		{
			Timer++;
			if (Timer % 330f < 180f)
			{
				base.Projectile.rotation = base.Projectile.rotation.AngleTowards(0f, 0.2f);
				float angle = MathHelper.ToRadians(2f * Timer % 180f);
				Vector2 destination = potentialTarget.Center - new Vector2((float)Math.Cos(angle) * (float)potentialTarget.width * 0.65f, 250f);
				base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, base.Projectile.SafeDirectionTo(destination) * 24f, 0.03f);
				if (Timer % 18f == base.Projectile.localAI[0] && potentialTarget.Top.Y > base.Projectile.Bottom.Y)
				{
					Vector2 laserVelocity = CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(base.Projectile.Center, potentialTarget, 25f, MidnightSunShot.MaxUpdate).RotatedByRandom(0.03999999910593033);
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Bottom, laserVelocity, ModContent.ProjectileType<MidnightSunShot>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				}
				base.Projectile.MinionAntiClump(0.35f);
				base.Projectile.ai[1] = 0f;
			}
			else
			{
				Vector2 hoverDestination = potentialTarget.Top - Vector2.UnitY * 40f + ((float)base.Projectile.minionPos + Timer / 7f).ToRotationVector2() * 40f;
				base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, hoverDestination, 0.1f).MoveTowards(hoverDestination, 20f);
				base.Projectile.velocity = base.Projectile.velocity.MoveTowards(Vector2.Zero, 4f);
				base.Projectile.ai[1] = Math.Abs(hoverDestination.Y - potentialTarget.Bottom.Y) + MathHelper.Lerp(30f, 50f, (float)base.Projectile.identity % 7f / 7f);
				if (Timer % 330f == 210f && Main.myPlayer == base.Projectile.owner)
				{
					SoundEngine.PlaySound(in SoundID.Item122, base.Projectile.Center);
					Vector2 laserVelocity2 = base.Projectile.velocity.RotatedBy(1.5707963705062866).SafeNormalize(Vector2.UnitY);
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, laserVelocity2, ModContent.ProjectileType<MidnightSunBeam>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, base.Projectile.whoAmI);
				}
			}
		}
		else
		{
			base.Projectile.velocity = (base.Projectile.velocity * 15f + base.Projectile.SafeDirectionTo(player.Center - new Vector2((float)player.direction * -80f, 160f)) * 19f) / 16f;
			Vector2 distanceVector = player.Center - base.Projectile.Center;
			if (((Vector2)(ref distanceVector)).Length() > 3900f)
			{
				base.Projectile.Center = player.Center;
				base.Projectile.netUpdate = true;
			}
			base.Projectile.MinionAntiClump(0.35f);
			base.Projectile.rotation = base.Projectile.velocity.X * 0.03f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = tex.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int i = 0; i < base.Projectile.oldPos.Length; i++)
			{
				Color trailColor = Color.Lerp(drawColor, Color.Transparent, (float)i / (float)base.Projectile.oldPos.Length);
				Vector2 trailPos = base.Projectile.oldPos[i] + base.Projectile.Size * 0.5f - Main.screenPosition;
				Main.EntitySpriteDraw(tex, trailPos, frame, trailColor, base.Projectile.rotation, frame.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
			}
		}
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, frame, drawColor, base.Projectile.rotation, frame.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
