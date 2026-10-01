using System;
using CalamityMod.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class WulfrumScrewdriverProj : ModProjectile
{
	public static int MaxTime = 14;

	public static Asset<Texture2D> SmearTex;

	public CalamityUtils.CurveSegment ThrustSegment = new CalamityUtils.CurveSegment(CalamityUtils.LinearEasing, 0f, 0f, 1f, 3);

	public CalamityUtils.CurveSegment HoldSegment = new CalamityUtils.CurveSegment(CalamityUtils.SineBumpEasing, 0.2f, 1f, 0.2f);

	public CalamityUtils.CurveSegment RetractSegment = new CalamityUtils.CurveSegment(CalamityUtils.PolyOutEasing, 0.76f, 1f, -0.8f, 3);

	public CalamityUtils.CurveSegment BumpSegment = new CalamityUtils.CurveSegment(CalamityUtils.SineBumpEasing, 0.9f, 0.2f, 0.15f);

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<WulfrumScrewdriver>();

	public override string Texture => "CalamityMod/Items/Weapons/Melee/WulfrumScrewdriver";

	public float Timer => MaxTime - base.Projectile.timeLeft;

	public float LifetimeCompletion => Timer / (float)MaxTime;

	public ref float EndLag => ref base.Projectile.ai[0];

	public ref float TrueDirection => ref base.Projectile.ai[1];

	public Player Owner => Main.player[base.Projectile.owner];

	internal float DistanceFromPlayer => CalamityUtils.PiecewiseAnimation(LifetimeCompletion, ThrustSegment, HoldSegment, RetractSegment, BumpSegment);

	public Vector2 OffsetFromPlayer
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.velocity * DistanceFromPlayer * 12f;
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = TrueMeleeDamageClass.Instance;
		base.Projectile.width = 14;
		base.Projectile.height = 50;
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.timeLeft = MaxTime;
	}

	public override bool? CanDamage()
	{
		return base.Projectile.timeLeft <= MaxTime - 5;
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		float collisionPoint = 0f;
		float bladeLength = 78f * base.Projectile.scale;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Owner.MountedCenter + OffsetFromPlayer, Owner.MountedCenter + OffsetFromPlayer + base.Projectile.velocity * bladeLength, 24f, ref collisionPoint);
	}

	public override void AI()
	{
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		if (EndLag == 0f)
		{
			EndLag = Math.Max(Owner.HeldItem.useTime - MaxTime, 1);
			TrueDirection = (Owner.Calamity().mouseWorld - Owner.MountedCenter).SafeNormalize(Vector2.Zero).ToRotation();
			base.Projectile.velocity = (Owner.Calamity().mouseWorld - Owner.MountedCenter).SafeNormalize(Vector2.Zero).RotatedByRandom(0.1178097352385521);
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		}
		base.Projectile.Center = Owner.MountedCenter + OffsetFromPlayer;
		base.Projectile.scale = 1f + (float)Math.Sin(LifetimeCompletion * (float)Math.PI) * 0.2f;
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.ChangeDir(MathF.Sign(base.Projectile.velocity.X));
		Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, base.Projectile.velocity.ToRotation() * Owner.gravDir - (float)Math.PI / 2f);
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile proj = enumerator.Current;
			if (proj.ModProjectile == null || proj.owner != base.Projectile.owner || !(proj.ModProjectile is WulfrumScrew screw) || screw.BazingaTime != 0f)
			{
				continue;
			}
			float collisionPoint = 0f;
			float bladeLength = 86f * base.Projectile.scale;
			if (Collision.CheckAABBvLineCollision(proj.Hitbox.TopLeft(), proj.Hitbox.Size(), Owner.Center + OffsetFromPlayer, Owner.Center + OffsetFromPlayer + base.Projectile.velocity * bladeLength, 34f, ref collisionPoint))
			{
				Vector2 thudVelocity = TrueDirection.ToRotationVector2() * 6f;
				NPC potentialAimAssist = FindTarget();
				if (potentialAimAssist != null)
				{
					thudVelocity = (potentialAimAssist.Center - proj.Center).SafeNormalize(Vector2.Zero) * 6f;
				}
				screw.BazingaTime = WulfrumScrew.BazingaTimeMax;
				proj.velocity = thudVelocity;
				if (screw.AlreadyBazinged == 0f)
				{
					proj.damage = (int)((float)proj.damage * WulfrumScrewdriver.ScrewBazingaModeDamageMult);
				}
				proj.timeLeft = WulfrumScrew.Lifetime;
				proj.knockBack *= 2.5f;
				screw.AlreadyBazinged++;
				SoundEngine.PlaySound(in WulfrumScrewdriver.ScrewHitSound, base.Projectile.Center);
				if (screw.AlreadyBazinged > 2f)
				{
					SoundEngine.PlaySound(in WulfrumScrewdriver.FunnyUltrablingSound, base.Projectile.Center);
				}
				if (Main.myPlayer == proj.owner)
				{
					Owner.SetScreenshake(6f);
				}
				break;
			}
		}
	}

	public NPC FindTarget()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		float bestScore = 0f;
		NPC bestTarget = null;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC potentialTarget = enumerator.Current;
			if (!potentialTarget.CanBeChasedBy())
			{
				continue;
			}
			float distance = potentialTarget.Distance(base.Projectile.Center);
			float angle = TrueDirection.ToRotationVector2().AngleBetween(potentialTarget.Center - base.Projectile.Center);
			float extraDistance = potentialTarget.width / 2 + potentialTarget.height / 2;
			if (distance - extraDistance < WulfrumScrewdriver.ScrewBazingaAimAssistReach && angle < WulfrumScrewdriver.ScrewBazingaAimAssistAngle / 2f && (Collision.CanHit(base.Projectile.Center, 1, 1, potentialTarget.Center, 1, 1) || !(extraDistance < distance)))
			{
				float attemptedScore = EvaluatePotentialTarget(distance - extraDistance, angle / 2f);
				if (attemptedScore > bestScore)
				{
					bestTarget = potentialTarget;
					bestScore = attemptedScore;
				}
			}
		}
		return bestTarget;
	}

	public float EvaluatePotentialTarget(float distance, float angle)
	{
		return 1f - distance / WulfrumScrewdriver.ScrewBazingaAimAssistReach * 0.2f + (1f - Math.Abs(angle) / (WulfrumScrewdriver.ScrewBazingaAimAssistAngle / 2f)) * 0.8f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in WulfrumScrewdriver.ThudSound, target.Center);
		base.Projectile.timeLeft = 0;
		if (Main.rand.NextBool(5) && Main.myPlayer == Owner.whoAmI && Owner.HeldItem.ModItem is WulfrumScrewdriver { ScrewStored: false })
		{
			WulfrumScrewdriver.ScrewStart = new Vector3(target.Center + base.Projectile.velocity * 14f * Main.rand.NextFloat() - Main.screenPosition, Main.rand.NextFloat((float)Math.PI / 4f));
			WulfrumScrewdriver.ScrewTimer = WulfrumScrewdriver.ScrewTime;
			WulfrumScrewdriver.ScrewQeuedForStorage = true;
			SoundEngine.PlaySound(in SoundID.Item156);
		}
		for (int k = 0; k < 4; k++)
		{
			Dust.NewDustPerfect(base.Projectile.Center + base.Projectile.velocity * 70f, 16, base.Projectile.velocity.RotatedByRandom(0.20000000298023224) * Main.rand.NextFloat(6f), 0, default(Color), Main.rand.NextFloat(0.7f, 1f));
		}
		base.OnHitNPC(target, hit, damageDone);
	}

	public override void OnKill(int timeLeft)
	{
		if (base.Projectile.numHits == 0)
		{
			Owner.itemTime = (int)EndLag;
			Owner.itemAnimation = (int)EndLag;
		}
		else
		{
			Owner.itemTime = 0;
			Owner.itemAnimation = 0;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		if (SmearTex == null)
		{
			SmearTex = ModContent.Request<Texture2D>("CalamityMod/Particles/MediumLongThrust", (AssetRequestMode)2);
		}
		Texture2D smearTex = SmearTex.Value;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector((float)tex.Width / 2f, (float)tex.Height);
		Vector2 scale = default(Vector2);
		((Vector2)(ref scale))._002Ector(Math.Abs((float)Math.Sin(LifetimeCompletion * ((float)Math.PI * 2f) * 0.5f)), 1f);
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, lightColor, base.Projectile.rotation, drawOrigin, scale * base.Projectile.scale, (SpriteEffects)0);
		if (LifetimeCompletion < 0.6f)
		{
			int frameCount = (int)Math.Floor(LifetimeCompletion / 0.6f * 3f);
			Rectangle frame = default(Rectangle);
			((Rectangle)(ref frame))._002Ector(0, smearTex.Height / 3 * frameCount, smearTex.Width, smearTex.Height / 3);
			float opacity = 1f - (float)Math.Pow(LifetimeCompletion / 0.6f, 0.5);
			Main.spriteBatch.Draw(smearTex, base.Projectile.Center + base.Projectile.velocity * 67f - Main.screenPosition, (Rectangle?)frame, Color.White * opacity, base.Projectile.rotation, frame.Size() / 2f, 0.9f, (SpriteEffects)0, 0f);
		}
		return false;
	}
}
