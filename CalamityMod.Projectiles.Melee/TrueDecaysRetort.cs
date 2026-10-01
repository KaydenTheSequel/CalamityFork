using System;
using System.IO;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class TrueDecaysRetort : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	public Vector2 direction;

	public bool ChargedUp;

	public const float LungeSpeed = 16f;

	public const float maxDash = 20f;

	private Vector2 PowerLungeStart;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/MendedBiomeBlade_DecaysRetort";

	public ref float MaxTime => ref base.Projectile.ai[0];

	public ref float CanLunge => ref base.Projectile.ai[1];

	public float Timer => MaxTime - (float)base.Projectile.timeLeft;

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float CanBounce => ref base.Projectile.localAI[0];

	public ref float dashTimer => ref base.Projectile.localAI[1];

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.width = (base.Projectile.height = 84);
		base.Projectile.width = (base.Projectile.height = 84);
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		float collisionPoint = 0f;
		float bladeLength = 140f * base.Projectile.scale;
		Vector2 displace = direction * ((float)Math.Sin(Timer / MaxTime * (float)Math.PI) * 60f);
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Owner.Center + displace, Owner.Center + displace + direction * bladeLength, 24f, ref collisionPoint);
	}

	public override void AI()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		if (!initialized)
		{
			CanBounce = 1f;
			base.Projectile.timeLeft = (int)MaxTime;
			direction = Owner.SafeDirectionTo(Owner.Calamity().mouseWorld, Vector2.Zero);
			((Vector2)(ref direction)).Normalize();
			base.Projectile.rotation = direction.ToRotation();
			if (CanLunge == 1f && !ChargedUp)
			{
				Lunge();
			}
			SoundEngine.PlaySound(in SoundID.Item103, base.Projectile.Center);
			initialized = true;
			base.Projectile.ForceNetUpdate();
		}
		if (ChargedUp && dashTimer == 0f)
		{
			PowerLunge();
		}
		if (dashTimer >= 1f)
		{
			if (dashTimer < 20f)
			{
				Owner.fallStart = (int)(Owner.position.Y / 16f);
				Owner.velocity = direction * 60f;
				base.Projectile.timeLeft = (int)(MaxTime / 2f);
				dashTimer++;
			}
			if (dashTimer == 20f)
			{
				Player owner = Owner;
				owner.velocity *= 0.1f;
				Owner.Calamity().LungingDown = false;
				SoundEngine.PlaySound(in CommonCalamitySounds.SwiftSliceSound, Owner.Center);
				if (Owner.whoAmI == Main.myPlayer && Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), Owner.Center - PowerLungeStart / 2f, Vector2.Zero, ModContent.ProjectileType<DecaysRetortDash>(), (int)((float)base.Projectile.damage * TrueBiomeBlade.EvilAttunement_SlashDamageBoost), 0f, Owner.whoAmI).ModProjectile is DecaysRetortDash dash)
				{
					dash.DashStart = PowerLungeStart;
					dash.DashEnd = Owner.Center;
				}
				dashTimer = 21f;
			}
		}
		base.Projectile.scale = 1f + (float)Math.Sin(Timer / MaxTime * (float)Math.PI) * 0.6f;
		base.Projectile.Center = Owner.Center + direction * ((float)Math.Sin(Timer / MaxTime * (float)Math.PI) * 60f);
		Lighting.AddLight(base.Projectile.Center, new Vector3(0.9f, 0f, 0.35f) * (float)Math.Sin(Timer / MaxTime * (float)Math.PI));
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.ChangeDir(Math.Sign(direction.X));
		Owner.itemRotation = direction.ToRotation();
		if (Owner.direction != 1)
		{
			Owner.itemRotation -= (float)Math.PI;
		}
		Owner.itemRotation = MathHelper.WrapAngle(Owner.itemRotation);
	}

	public void Lunge()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Owner.velocity = direction.SafeNormalize(Vector2.UnitX * (float)Owner.direction) * 16f;
		}
	}

	public void PowerLunge()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		Owner.Calamity().LungingDown = true;
		PowerLungeStart = Owner.Center;
		dashTimer = 1f;
		Owner.GiveUniversalIFrames(TrueBiomeBlade.EvilAttunement_SlashIFrames);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		if (ChargedUp)
		{
			return;
		}
		base.Projectile.ForceNetUpdate();
		if (Main.myPlayer != Owner.whoAmI || CanBounce == 0f)
		{
			return;
		}
		Owner.DoLifestealDirect(target, TrueBiomeBlade.EvilAttunement_Lifesteal);
		float bounceStrength = 8f;
		bounceStrength *= ((Owner.velocity.Y == 0f) ? 0.2f : 1f);
		Owner.velocity = -direction.SafeNormalize(Vector2.Zero) * MathHelper.Clamp(bounceStrength, 0f, 22f);
		CanBounce = 0f;
		Owner.GiveUniversalIFrames(TrueBiomeBlade.EvilAttunement_BounceIFrames);
		if (Owner.whoAmI == Main.myPlayer && Owner.HeldItem.ModItem is TrueBiomeBlade sword)
		{
			sword.PowerLungeCounter++;
			if (sword.PowerLungeCounter == 3)
			{
				SoundEngine.PlaySound(in SoundID.Item79);
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		Texture2D handle = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/MendedBiomeBlade", (AssetRequestMode)2).Value;
		Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/MendedBiomeBlade_DecaysRetort", (AssetRequestMode)2).Value;
		float drawRotation = direction.ToRotation() + (float)Math.PI / 4f;
		Vector2 displace = direction * ((float)Math.Sin(Timer / MaxTime * (float)Math.PI) * 60f);
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector(0f, (float)handle.Height);
		Vector2 drawOffset = Owner.Center + direction * 10f - Main.screenPosition;
		Main.EntitySpriteDraw(handle, drawOffset + displace, null, lightColor, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		((Vector2)(ref drawOrigin))._002Ector(0f, (float)tex.Height);
		Main.EntitySpriteDraw(tex, drawOffset + displace, null, Color.Lerp(Color.White, lightColor, 0.5f) * 0.9f, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		if (dashTimer > 0f && dashTimer < 20f)
		{
			float thrustRatio = (float)Math.Sin(dashTimer / 20f * (float)Math.PI);
			Main.EntitySpriteDraw(tex, drawOffset + displace, null, Color.Lerp(Color.White, lightColor, 0.5f) * 0.9f, drawRotation, drawOrigin, base.Projectile.scale * (1f + thrustRatio * 0.2f), (SpriteEffects)0);
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
		writer.Write(CanBounce);
		writer.Write(ChargedUp);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		initialized = reader.ReadBoolean();
		direction = reader.ReadVector2();
		CanBounce = reader.ReadSingle();
		ChargedUp = reader.ReadBoolean();
	}

	public TrueDecaysRetort()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		direction = Vector2.Zero;
		base._002Ector();
	}
}
