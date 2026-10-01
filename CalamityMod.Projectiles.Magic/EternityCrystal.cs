using System;
using System.IO;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class EternityCrystal : ModProjectile, ILocalizedModType, IModType
{
	public bool Collapsing;

	public float TargetOffsetRadius = 480f;

	public float DegreesToSpin = 2f;

	public const int InwardCollapseTime = 70;

	public new string LocalizationCategory => "Projectiles.Magic";

	public int TargetIndex
	{
		get
		{
			return (int)base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public float SpinAngle
	{
		get
		{
			return base.Projectile.ai[1];
		}
		set
		{
			base.Projectile.ai[1] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 34;
		base.Projectile.height = 36;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 310;
		base.Projectile.alpha = 0;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(Collapsing);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		Collapsing = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (base.Projectile.localAI[1] >= (float)Main.projectile.Length || base.Projectile.localAI[0] < 0f)
		{
			DeathDust();
			base.Projectile.Kill();
			return;
		}
		if (!Main.projectile[(int)base.Projectile.localAI[1]].active)
		{
			DeathDust();
			base.Projectile.Kill();
			return;
		}
		if (TargetIndex >= Main.npc.Length || TargetIndex < 0)
		{
			DeathDust();
			base.Projectile.Kill();
			return;
		}
		NPC target = Main.npc[TargetIndex];
		if (!target.active)
		{
			DeathDust();
			base.Projectile.Kill();
			return;
		}
		base.Projectile.localAI[0]++;
		if (base.Projectile.timeLeft == 1 && !Collapsing)
		{
			base.Projectile.velocity = base.Projectile.SafeDirectionTo(target.Center) * 2f;
			base.Projectile.timeLeft = 70;
			Collapsing = true;
			base.Projectile.netUpdate = true;
		}
		SpinAngle -= MathHelper.ToRadians(DegreesToSpin);
		base.Projectile.rotation = base.Projectile.AngleTo(target.Center) - (float)Math.PI / 2f;
		base.Projectile.position = target.Center + SpinAngle.ToRotationVector2() * TargetOffsetRadius;
		if (!Collapsing)
		{
			base.Projectile.damage = 0;
			return;
		}
		DegreesToSpin *= 1.0425f;
		TargetOffsetRadius *= 0.95f;
		Rectangle hitbox = base.Projectile.Hitbox;
		if (((Rectangle)(ref hitbox)).Intersects(target.Hitbox) && !target.dontTakeDamage)
		{
			ExplosionEffect(target, player);
		}
		if (base.Projectile.alpha < 255)
		{
			base.Projectile.alpha += 3;
		}
	}

	public void DeathDust()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		Color newColor = default(Color);
		for (int i = 0; i < 20; i++)
		{
			Vector2 center = base.Projectile.Center;
			((Color)(ref newColor))._002Ector(245, 112, 218);
			Dust dust = Dust.NewDustPerfect(center, 16, null, 0, newColor);
			dust.velocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(2f, 6f);
			dust.noGravity = true;
		}
	}

	public void ExplosionEffect(NPC target, Player player)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		int damage = (int)player.GetTotalDamage<MagicDamageClass>().ApplyTo(8400f);
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), damage, 0f, base.Projectile.owner, target.whoAmI);
		Vector2 randomCirclePointVector = Vector2.UnitY.RotatedBy(base.Projectile.rotation);
		int pointsPerStarStrip = 20;
		int starPoints = 9;
		float minStarOutwardness = Main.rand.NextFloat(12f, 28f);
		float maxStarOutwardness = Main.rand.NextFloat(48f, 68f);
		for (float i = 0f; i < (float)starPoints; i++)
		{
			for (int rotationDirection = -1; rotationDirection <= 1; rotationDirection += 2)
			{
				Vector2 randomCirclePointRotated = randomCirclePointVector.RotatedBy((float)rotationDirection * ((float)Math.PI * 2f) / (float)(starPoints * 2));
				for (float k = 0f; k < (float)pointsPerStarStrip; k++)
				{
					Vector2 randomCirclePointLerped = Vector2.Lerp(randomCirclePointVector, randomCirclePointRotated, k / (float)pointsPerStarStrip);
					float outwardness = MathHelper.Lerp(minStarOutwardness, maxStarOutwardness, k / (float)pointsPerStarStrip) * 2f;
					Dust dust = Dust.NewDustDirect(new Vector2(target.Center.X, target.Center.Y), 0, 0, Main.rand.Next(132, 134), 0f, 0f, 100, default(Color), 1.3f);
					dust.velocity *= 0.1f;
					dust.velocity += randomCirclePointLerped * outwardness;
					dust.noGravity = true;
					dust.color = Utils.SelectRandom(Main.rand, (Color[])(object)new Color[2]
					{
						new Color(61, 141, 235),
						new Color(229, 52, 220)
					});
				}
			}
			randomCirclePointVector = randomCirclePointVector.RotatedBy((float)Math.PI * 2f / (float)starPoints);
		}
		SoundEngine.PlaySound(in CommonCalamitySounds.LargeWeaponFireSound, target.Center);
		base.Projectile.Kill();
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		Texture2D myTexture = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = myTexture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Color trasparentCrystalColor = base.Projectile.GetAlpha(lightColor) * 0.6f;
		Vector2 origin = frame.Size() / 2f;
		float outwardness = MathHelper.Lerp(2f, 5f, (float)Math.Cos(Main.GlobalTimeWrappedHourly) * 0.5f + 0.5f);
		for (float i = 0f; i < 5f; i++)
		{
			float angle = (float)Math.PI * 2f / 5f * i + (float)Math.PI / 2f;
			Vector2 offset = Vector2.UnitY.RotatedBy(angle).RotatedBy(base.Projectile.rotation);
			Vector2 drawPosition = base.Projectile.Center - Main.screenPosition + offset * outwardness + Vector2.UnitY * base.Projectile.gfxOffY;
			Main.EntitySpriteDraw(myTexture, drawPosition, frame, trasparentCrystalColor, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		}
		return true;
	}
}
