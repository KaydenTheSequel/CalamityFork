using System;
using System.IO;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class AridGrandeur : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	private Vector2 direction;

	public const float pogoStrenght = 16f;

	public const float maxShred = 500f;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/BrokenBiomeBlade_AridGrandeur";

	public ref float Shred => ref base.Projectile.ai[0];

	public float ShredRatio => MathHelper.Clamp(Shred / 250f, 0f, 1f);

	public ref float PogoCooldown => ref base.Projectile.ai[1];

	public Player Owner => Main.player[base.Projectile.owner];

	public bool CanPogo
	{
		get
		{
			if (Owner.velocity.Y != 0f)
			{
				return PogoCooldown <= 0f;
			}
			return false;
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.width = (base.Projectile.height = 70);
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = BrokenBiomeBlade.HotAttunement_LocalIFrames;
		base.Projectile.timeLeft = BrokenBiomeBlade.HotAttunement_LocalIFrames;
	}

	public override bool? CanDamage()
	{
		return base.Projectile.timeLeft <= 2;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		float collisionPoint = 0f;
		float bladeLength = 94f * base.Projectile.scale;
		float bladeWidth = 76f * base.Projectile.scale;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Owner.Center, Owner.Center + direction * bladeLength, bladeWidth, ref collisionPoint);
	}

	public void Pogo()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		if (CanPogo && Main.myPlayer == Owner.whoAmI)
		{
			Owner.velocity = -direction.SafeNormalize(Vector2.Zero) * 16f;
			Owner.fallStart = (int)(Owner.position.Y / 16f);
			PogoCooldown = 30f;
			SoundEngine.PlaySound(in SoundID.DD2_MonkStaffGroundImpact, base.Projectile.position);
			Vector2 hitPosition = Owner.Center + direction * 84f * base.Projectile.scale;
			for (int i = 0; i < 8; i++)
			{
				Vector2 hitPositionDisplace = direction.RotatedBy(1.5707963705062866) * Main.rand.NextFloat(-10f, 10f);
				Vector2 flyDirection = -direction.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 4f, (float)Math.PI / 4f));
				GeneralParticleHandler.SpawnParticle(new SmallSmokeParticle(hitPosition + hitPositionDisplace, flyDirection * 9f, Color.OrangeRed, new Color(130, 130, 130), Main.rand.NextFloat(1.8f, 2.6f), 115 - Main.rand.Next(30)));
				GeneralParticleHandler.SpawnParticle(new StrongBloom(hitPosition - hitPositionDisplace * 3f, -direction * 6f * Main.rand.NextFloat(0.5f, 1f), Color.Orange * 0.5f, 0.01f + Main.rand.NextFloat(0f, 0.2f), 20 + Main.rand.Next(40)));
			}
			for (int j = 0; j < 3; j++)
			{
				Vector2 hitPositionDisplace2 = direction.RotatedBy(1.5707963705062866) * Main.rand.NextFloat(-10f, 10f);
				Vector2 flyDirection2 = -direction.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 4f, (float)Math.PI / 4f));
				GeneralParticleHandler.SpawnParticle(new StoneDebrisParticle(hitPosition - hitPositionDisplace2 * 3f, flyDirection2 * Main.rand.NextFloat(3f, 6f), Color.Beige, 1f + Main.rand.NextFloat(0f, 0.4f), 30 + Main.rand.Next(50), 0.1f));
			}
			if (Owner.HeldItem.type == ModContent.ItemType<BrokenBiomeBlade>())
			{
				(Owner.HeldItem.ModItem as BrokenBiomeBlade).CanLunge = 1;
			}
		}
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		if (!initialized)
		{
			SoundEngine.PlaySound(in SoundID.Item90, base.Projectile.Center);
			initialized = true;
		}
		if (Owner.CantUseHoldout())
		{
			base.Projectile.Kill();
			return;
		}
		if (Shred >= 500f)
		{
			Shred = 500f;
		}
		if (Shred < 0f)
		{
			Shred = 0f;
		}
		Lighting.AddLight(base.Projectile.Center, new Vector3(1f, 0.56f, 0.56f) * ShredRatio);
		direction = Owner.SafeDirectionTo(Owner.Calamity().mouseWorld, Vector2.Zero);
		((Vector2)(ref direction)).Normalize();
		base.Projectile.rotation = direction.ToRotation();
		base.Projectile.Center = Owner.Center + direction * 60f;
		base.Projectile.scale = 1.33f + ShredRatio * 1f;
		if (Collision.SolidCollision(Owner.Center + direction * 84f * base.Projectile.scale - Vector2.One * 5f, 10, 10))
		{
			Pogo();
			base.Projectile.ForceNetUpdate();
		}
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.ChangeDir(Math.Sign(direction.X));
		Owner.itemRotation = direction.ToRotation();
		if (Owner.direction != 1)
		{
			Owner.itemRotation -= (float)Math.PI;
		}
		Owner.itemRotation = MathHelper.WrapAngle(Owner.itemRotation);
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		Shred -= BrokenBiomeBlade.HotAttunement_ShredDecayRate;
		PogoCooldown--;
		if (base.Projectile.timeLeft <= 2)
		{
			base.Projectile.timeLeft = 2;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		ShredTarget();
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		ShredTarget();
	}

	private void ShredTarget()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == Owner.whoAmI && PogoCooldown <= 0f)
		{
			SoundEngine.PlaySound(in SoundID.NPCHit30, base.Projectile.Center);
			Shred += 62f;
			if (Owner.velocity.Y > 0f)
			{
				Owner.velocity.Y = -2f;
			}
			Owner.GiveUniversalIFrames(BrokenBiomeBlade.HotAttunement_ShredPlayerIFrames);
			PogoCooldown = 20f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.NPCHit43, base.Projectile.Center);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		Texture2D handle = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/BrokenBiomeBlade", (AssetRequestMode)2).Value;
		Texture2D blade = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/BrokenBiomeBlade_AridGrandeur", (AssetRequestMode)2).Value;
		int bladeAmount = 4;
		float drawAngle = direction.ToRotation();
		float drawRotation = drawAngle + (float)Math.PI / 4f;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector(0f, (float)handle.Height);
		Vector2 drawOffset = Owner.Center + direction * 10f - Main.screenPosition;
		Main.EntitySpriteDraw(handle, drawOffset, null, lightColor, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		((Vector2)(ref drawOrigin))._002Ector(0f, (float)blade.Height);
		drawOffset = Owner.Center + drawAngle.ToRotationVector2() * 32f * base.Projectile.scale - Main.screenPosition;
		Main.EntitySpriteDraw(blade, drawOffset, null, Color.Lerp(Color.White, lightColor, 0.5f) * 0.9f, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		for (int i = 0; i < bladeAmount; i++)
		{
			blade = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/BrokenBiomeBlade_AridGrandeurExtra", (AssetRequestMode)2).Value;
			drawAngle = direction.ToRotation();
			float circleCompletion = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 5f + (float)i * ((float)Math.PI / 2f));
			drawRotation = drawAngle + (float)Math.PI / 4f + circleCompletion * (float)Math.PI / 10f - circleCompletion * ((float)Math.PI / 7f) * ShredRatio;
			((Vector2)(ref drawOrigin))._002Ector(0f, (float)blade.Height);
			Vector2 drawOffsetStraight = Owner.Center + direction * (float)Math.Sin(Main.GlobalTimeWrappedHourly * 7f) * 10f - Main.screenPosition;
			Vector2 drawDisplacementAngle = direction.RotatedBy(1.5707963705062866) * circleCompletion.ToRotationVector2().Y * (20f + 40f * ShredRatio);
			Main.EntitySpriteDraw(blade, drawOffsetStraight + drawDisplacementAngle, null, Color.Lerp(Color.White, lightColor, 0.5f) * 0.8f, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		}
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		return false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(initialized);
		writer.WriteVector2(direction);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		initialized = reader.ReadBoolean();
		direction = reader.ReadVector2();
	}

	public AridGrandeur()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		direction = Vector2.Zero;
		base._002Ector();
	}
}
