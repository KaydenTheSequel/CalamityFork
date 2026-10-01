using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class PulseDragonProjectile : ModProjectile, ILocalizedModType, IModType
{
	public const int ChargeTime = 25;

	public const int ReelbackTime = 25;

	public const int Lifetime = 50;

	public const float MaximumPossibleOutwardness = 72f;

	public new string LocalizationCategory => "Projectiles.Misc";

	public bool ReelingBack
	{
		get
		{
			return base.Projectile.timeLeft <= 25;
		}
		set
		{
			if (value)
			{
				base.Projectile.timeLeft = 25;
			}
		}
	}

	public Player Owner => Main.player[base.Projectile.owner];

	public float Outwardness => OutwardnessMax * (float)Math.Sin((float)base.Projectile.timeLeft / 50f * (float)Math.PI);

	public ref float InitialRotation => ref base.Projectile.ai[0];

	public ref float OutwardnessMax => ref base.Projectile.ai[1];

	public ref float SwingDirection => ref base.Projectile.localAI[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.timeLeft = 50;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 4;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 1;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(SwingDirection);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		SwingDirection = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.AngleTo(Owner.Center) - (float)Math.PI / 2f;
		if (Owner.dead)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.direction = (Owner.Center.X - base.Projectile.Center.X > 0f).ToDirectionInt();
		base.Projectile.spriteDirection = base.Projectile.direction;
		ManipulateOwnerFields();
		if (base.Projectile.timeLeft >= 25)
		{
			float time = Utils.GetLerpValue(50f, 25f, base.Projectile.timeLeft, clamped: true);
			float offsetAngle = MathHelper.Lerp(-1.1f, 1.5f, time);
			offsetAngle *= base.Projectile.localAI[0];
			base.Projectile.velocity = InitialRotation.ToRotationVector2().RotatedBy(offsetAngle) * 29f;
			if (Vector2.Dot(base.Projectile.velocity.SafeNormalize(Vector2.Zero), Owner.velocity.SafeNormalize(Vector2.Zero)) > 0.45f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity += Owner.velocity;
			}
		}
		else
		{
			base.Projectile.velocity = base.Projectile.SafeDirectionTo(Owner.Center, Vector2.UnitX * (float)Owner.direction) * 43f;
			base.Projectile.timeLeft = 25;
			if (base.Projectile.WithinRange(Owner.Center, 45f))
			{
				base.Projectile.Kill();
			}
		}
		GenerateIdleDust();
		if (base.Projectile.timeLeft % 3 == 2 && base.Projectile.Distance(Owner.Center) > 40f)
		{
			SpawnElectricFields();
		}
	}

	public void ManipulateOwnerFields()
	{
		Owner.itemAnimation = 4;
		Owner.itemTime = 4;
		Owner.ChangeDir(-base.Projectile.direction);
		Owner.itemRotation = base.Projectile.rotation + (float)(base.Projectile.spriteDirection == -1).ToInt() * ((float)Math.PI * 2f);
	}

	public void GenerateIdleDust()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			for (int i = 0; i < 4; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(52f, 52f), 261);
				dust.velocity = Vector2.Zero;
				dust.noGravity = true;
			}
		}
	}

	public void SpawnElectricFields()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Projectile field = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, 443, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			if (field.whoAmI.WithinBounds(Main.maxProjectiles))
			{
				field.DamageType = DamageClass.MeleeNoSpeed;
				field.usesIDStaticNPCImmunity = false;
				field.usesLocalNPCImmunity = true;
				field.localNPCHitCooldown = 3;
				field.timeLeft = 12;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		Texture2D chainTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/PulseDragonChain", (AssetRequestMode)2).Value;
		Texture2D pulseTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/SmallGreyscaleCircle", (AssetRequestMode)2).Value;
		Texture2D dragonHeadTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/PulseDragonProjectile", (AssetRequestMode)2).Value;
		if (ReelingBack)
		{
			dragonHeadTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/PulseDragonHeadClosed", (AssetRequestMode)2).Value;
		}
		Vector2 mountedCenter = Main.player[base.Projectile.owner].MountedCenter;
		List<Vector2> bezierPoints = new List<Vector2> { mountedCenter };
		for (int i = 0; i < 20; i++)
		{
			Vector2 offset = Vector2.UnitX * (0f - SwingDirection);
			offset *= Outwardness * (float)Math.Sin((float)i / 20f * (float)Math.PI);
			offset *= Utils.GetLerpValue(0f, 300f, Owner.Distance(Vector2.Lerp(mountedCenter, base.Projectile.Center, (float)i / 20f) + offset), clamped: true);
			bezierPoints.Add(Vector2.Lerp(mountedCenter, base.Projectile.Center, (float)i / 20f) + offset);
		}
		bezierPoints.Add(base.Projectile.Center);
		BezierCurve bezierCurve = new BezierCurve(bezierPoints.ToArray());
		int totalChains = (int)(base.Projectile.Distance(mountedCenter) / (float)chainTexture.Height);
		totalChains = (int)MathHelper.Clamp((float)totalChains, 40f, 1000f);
		for (int j = 0; j < totalChains - 1; j++)
		{
			Vector2 drawPosition = bezierCurve.Evaluate((float)j / (float)totalChains);
			float angle = (bezierCurve.Evaluate((float)j / (float)totalChains + 1f / (float)totalChains) - drawPosition).ToRotation();
			angle -= (float)Math.PI / 2f;
			Main.EntitySpriteDraw(chainTexture, drawPosition - Main.screenPosition, null, lightColor, angle, chainTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		}
		for (int k = 0; k < 5; k++)
		{
			Vector2 offset2 = ((float)k / 5f * ((float)Math.PI * 2f)).ToRotationVector2() * 24f;
			float time = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 1.8f);
			float angle2 = time * (float)Math.PI + Main.GlobalTimeWrappedHourly * 2.1f;
			float scale = 1.1f + time * 0.2f;
			Main.EntitySpriteDraw(pulseTexture, base.Projectile.Center + offset2 - Main.screenPosition, null, Color.Cyan * 0.3f, angle2, pulseTexture.Size() * 0.5f, scale, (SpriteEffects)0);
		}
		_ = base.Projectile.spriteDirection;
		_ = -1;
		Main.EntitySpriteDraw(dragonHeadTexture, base.Projectile.Center - Main.screenPosition, null, lightColor, base.Projectile.rotation, dragonHeadTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
