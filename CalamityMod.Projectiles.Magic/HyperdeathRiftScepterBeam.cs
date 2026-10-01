using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class HyperdeathRiftScepterBeam : ModProjectile, ILocalizedModType, IModType
{
	public float time;

	public bool doneAttack;

	public int attackTime;

	public float laserFX;

	public float storedTime;

	public Color drawColor;

	public float sine;

	public float laserRot;

	private Vector2 beamStart;

	private Vector2 directionToTarget;

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float attackSpeed => ref base.Projectile.ai[0];

	public ref float laserType => ref base.Projectile.ai[2];

	public bool canDamage
	{
		get
		{
			if (doneAttack)
			{
				return laserFX >= 1f;
			}
			return false;
		}
	}

	public float laserLength => 3000f;

	public Vector2 targetPos
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Center;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 1;
		base.Projectile.height = 1;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 6000;
		base.Projectile.scale = 2f;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		if (laserFX > 0f)
		{
			laserFX = MathHelper.Lerp(laserFX, 0f, (time > 15f) ? 0.07f : 0.01f);
		}
		sine = (float)Math.Sin(time * 4f / (float)Math.PI);
		if (time == 0f)
		{
			laserType = 1f;
			if (CalamityWorld.LegendaryMode)
			{
				base.Projectile.scale = 6f;
			}
			laserRot = Main.rand.NextFloat(-0.4f, 0.4f);
			beamStart = targetPos + ((laserType == 0f || laserType == 4f || laserType == 5f) ? Vector2.UnitX : Vector2.UnitY).RotatedBy(laserRot) * laserLength;
			directionToTarget = beamStart.DirectionTo(targetPos);
			Projectile projectile = base.Projectile;
			projectile.Center += Main.rand.NextVector2CircularEdge(400f, 400f);
			if (attackSpeed == 0f)
			{
				attackSpeed = 0.5f;
			}
			base.Projectile.velocity = Vector2.Zero;
			laserFX = 1f;
			base.Projectile.ForceNetUpdate();
		}
		if (time >= (float)attackTime && !doneAttack)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/DoGLaserWallBigAttack");
			style.Volume = 0.6f;
			style.Pitch = 0f;
			style.MaxInstances = -1;
			SoundEngine.PlaySound(in style, targetPos);
			laserFX = 2.5f;
			doneAttack = true;
			storedTime = time;
			base.Projectile.ForceNetUpdate();
			if (Main.LocalPlayer.Distance(base.Projectile.Center) < 1600f)
			{
				Main.LocalPlayer.SetScreenshake(3f);
			}
		}
		float endTime = storedTime + 10f;
		if (time >= endTime && doneAttack)
		{
			base.Projectile.Kill();
			return;
		}
		if (doneAttack)
		{
			drawColor = Color.Lerp(Color.Magenta, Color.Cyan, (float)Math.Pow(Utils.GetLerpValue(endTime, storedTime, time, clamped: true), 2.0));
		}
		time += attackSpeed;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (canDamage)
		{
			return null;
		}
		return false;
	}

	public override bool CanHitPlayer(Player target)
	{
		if (canDamage)
		{
			return true;
		}
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		base.OnHitPlayer(target, info);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 180);
		base.OnHitNPC(target, hit, damageDone);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		if (!canDamage)
		{
			return false;
		}
		float _ = float.NaN;
		Vector2 start = beamStart;
		Vector2 end = beamStart + directionToTarget * laserLength * 2f;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, 70f * base.Projectile.scale, ref _);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		if (laserFX == 0f)
		{
			return false;
		}
		Texture2D beam = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomLineThick", (AssetRequestMode)2).Value;
		Texture2D bBeam = ModContent.Request<Texture2D>("CalamityMod/Particles/LineThick", (AssetRequestMode)2).Value;
		_ = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		float opacity = (doneAttack ? 0.65f : 0.35f) * (float)Math.Pow(Math.Min(laserFX, 1f), 2.0);
		Color val = drawColor;
		((Color)(ref val)).A = 0;
		Color beamColor = val;
		if (CalamityClientConfig.Instance.Photosensitivity)
		{
			opacity = 0.2f;
		}
		for (int t = 0; (float)t < ((!doneAttack) ? 1f : (4f * base.Projectile.scale)); t++)
		{
			bool black = (float)t > 0f + (base.Projectile.scale - 1f);
			if (black)
			{
				beam = bBeam;
			}
			float beamThickness = 0.09f * (black ? (0.8f - 0.15f * (float)t / base.Projectile.scale) : 1f) * ((laserFX <= 1f) ? ((float)Math.Pow(Math.Min(laserFX, 1f), 2.0)) : laserFX) * Utils.Remap(sine, -1f, 1f, 0.8f, 1.1f);
			Main.EntitySpriteDraw(beam, beamStart - Main.screenPosition, null, (black ? (Color.Black * opacity) : (beamColor * (1f - (float)t * 0.3f) * opacity)) * (black ? (0.2f + 0.15f * (float)t / base.Projectile.scale) : 1f), directionToTarget.ToRotation() + (float)Math.PI / 2f, new Vector2((float)(beam.Width / 2), (float)beam.Height), new Vector2(beamThickness, laserLength / 975f) * base.Projectile.scale, (SpriteEffects)0);
		}
		return false;
	}

	public HyperdeathRiftScepterBeam()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		attackTime = 10;
		drawColor = Color.Magenta;
		beamStart = Vector2.Zero;
		directionToTarget = Vector2.Zero;
		base._002Ector();
	}
}
