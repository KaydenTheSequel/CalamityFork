using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using CalamityMod.Graphics.Primitives;
using CalamityMod.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.BaseProjectiles;

[PierceResistException(false)]
public abstract class BaseSwordHoldoutProjectile : ModProjectile
{
	[CompilerGenerated]
	private Color _003CAfterImageColor_003Ek__BackingField;

	[CompilerGenerated]
	private Vector2 _003Cangle_003Ek__BackingField;

	[CompilerGenerated]
	private Vector2 _003ColdPlayerOffset_003Ek__BackingField;

	internal int swingTimer;

	public static Asset<Texture2D> TrailTexture;

	public float baseScale;

	public List<float> oldScale;

	private List<float> oldProjectileRot;

	private List<Vector2> oldProjectilePos;

	internal int ExistsTime;

	private bool hasFakedOnSpawn;

	public virtual int swingWidth { get; set; }

	public virtual int swingTime { get; set; }

	public virtual bool AlternateSwings { get; set; }

	public virtual int OffsetDistance { get; set; }

	public virtual Item BaseItem { get; set; }

	public virtual bool UsesBaseItem { get; set; }

	public virtual int AfterImageLength { get; set; }

	public virtual bool useMeleeSpeed { get; set; }

	public virtual bool useMeleeSize { get; set; }

	public virtual Color[] trailColors { get; set; }

	public virtual bool drawSwordTrail { get; set; }

	public virtual float trailOffset { get; set; }

	public virtual int trailLength { get; set; }

	public virtual int StartupTime { get; set; }

	public virtual int CooldownTime { get; set; }

	public virtual float RotateInStartup { get; set; }

	public virtual float RotateInCooldown { get; set; }

	public virtual SoundStyle? UseSound { get; set; }

	public virtual float lineCollisionLength { get; set; }

	public virtual Color AfterImageColor
	{
		[CompilerGenerated]
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _003CAfterImageColor_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			_003CAfterImageColor_003Ek__BackingField = value;
		}
	}

	public Vector2 angle
	{
		[CompilerGenerated]
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _003Cangle_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			_003Cangle_003Ek__BackingField = value;
		}
	}

	public Vector2 oldPlayerOffset
	{
		[CompilerGenerated]
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _003ColdPlayerOffset_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			_003ColdPlayerOffset_003Ek__BackingField = value;
		}
	}

	public int timer { get; set; }

	public bool inStartup => timer < StartupTime;

	public bool inCooldown => timer > CooldownStartFrame;

	public bool inSwing
	{
		get
		{
			if (!inStartup)
			{
				return !inCooldown;
			}
			return false;
		}
	}

	public int CooldownStartFrame => swingTime + StartupTime;

	public int CooldownTimer => timer - CooldownStartFrame;

	public float StartupCompletion => (float)timer / (float)StartupTime;

	public float SwingCompletion => (float)swingTimer / (float)swingTime;

	public float CooldownCompletion => (float)CooldownTimer / (float)CooldownTime;

	public virtual void AdditionalAI()
	{
	}

	public virtual void Spawn()
	{
	}

	public virtual void Defaults()
	{
	}

	public virtual float SwingFunction()
	{
		return MathHelper.ToRadians(MathHelper.SmoothStep((float)(-swingWidth / 2), (float)(swingWidth / 2), (float)swingTimer / (float)swingTime));
	}

	public virtual float trailWidth(float completion, Vector2 vertexPos)
	{
		return MathHelper.Lerp(30f, 0f, completion);
	}

	public virtual Color trailColor(float completion, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.Black;
	}

	public override void SetDefaults()
	{
		base.Projectile.timeLeft = swingTime * 2;
		if (UsesBaseItem)
		{
			base.Projectile.width = (base.Projectile.height = Math.Max(BaseItem.height, BaseItem.width));
		}
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.extraUpdates = 0;
		base.Projectile.aiStyle = -2;
		base.Projectile.DamageType = ModLoader.GetMod("CalamityMod").Find<DamageClass>("TrueMeleeDamageClass");
		base.Projectile.tileCollide = false;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 100;
		Defaults();
	}

	private void FakeOnSpawn()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		angle = (player.MountedCenter - player.Calamity().mouseWorld).SafeNormalize(Vector2.One);
		base.Projectile.velocity = Vector2.Zero;
		if (angle.X < 0f)
		{
			player.direction = 1;
			base.Projectile.spriteDirection = (int)player.gravDir;
		}
		else
		{
			player.direction = -1;
			base.Projectile.spriteDirection = -1 * (int)player.gravDir;
		}
		if (AlternateSwings && player.GetModPlayer<BaseSwordHoldoutPlayer>().swingNum % 2 == 1)
		{
			base.Projectile.spriteDirection *= -1;
		}
		if (AlternateSwings)
		{
			player.GetModPlayer<BaseSwordHoldoutPlayer>().swingNum++;
		}
		swingTime = Main.player[base.Projectile.owner].HeldItem.useTime;
		Spawn();
		StartupTime *= base.Projectile.MaxUpdates;
		CooldownTime *= base.Projectile.MaxUpdates;
		swingTime *= base.Projectile.MaxUpdates;
		if (useMeleeSpeed)
		{
			float speed = Main.player[base.Projectile.owner].GetAttackSpeed<MeleeDamageClass>();
			if (speed > 3f)
			{
				speed = 3f;
			}
			if (speed != 0f)
			{
				speed = 1f / speed;
			}
			swingTime = (int)((float)swingTime * speed);
			if (swingTime < 1)
			{
				swingTime = 1;
			}
			StartupTime = (int)((float)StartupTime * speed);
			CooldownTime = (int)((float)CooldownTime * speed);
		}
		if (useMeleeSize)
		{
			if (player.meleeScaleGlove)
			{
				base.Projectile.scale *= 1.1f;
			}
			base.Projectile.scale *= player.HeldItem.scale;
		}
		baseScale = base.Projectile.scale;
		ExistsTime = swingTime + StartupTime + CooldownTime;
		base.Projectile.timeLeft = ExistsTime * 2;
		base.Projectile.netUpdate = true;
	}

	public override void AI()
	{
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		if (!hasFakedOnSpawn)
		{
			FakeOnSpawn();
			hasFakedOnSpawn = true;
		}
		Player player = Main.player[base.Projectile.owner];
		base.Projectile.gfxOffY = player.gfxOffY;
		player.Calamity().mouseWorldListener = true;
		BaseSwordHoldoutPlayer modplayer = player.GetModPlayer<BaseSwordHoldoutPlayer>();
		float adust = MathHelper.ToRadians(225f);
		if (timer < StartupTime || timer > StartupTime + swingTime)
		{
			if (inStartup)
			{
				angle = Vector2.Lerp(angle, (player.MountedCenter - player.Calamity().mouseWorld).SafeNormalize(Vector2.One), RotateInStartup);
			}
			if (inCooldown)
			{
				angle = Vector2.Lerp(angle, (player.MountedCenter - player.Calamity().mouseWorld).SafeNormalize(Vector2.One), RotateInCooldown);
			}
			if (angle.X < 0f)
			{
				player.direction = 1;
				base.Projectile.spriteDirection = (int)player.gravDir;
			}
			else
			{
				player.direction = -1;
				base.Projectile.spriteDirection = -1 * (int)player.gravDir;
			}
			if (AlternateSwings && player.GetModPlayer<BaseSwordHoldoutPlayer>().swingNum % 2 == 1)
			{
				base.Projectile.spriteDirection *= -1;
			}
		}
		if (base.Projectile.spriteDirection == -1)
		{
			adust = MathHelper.ToRadians(-45f);
		}
		Vector2 armCenter = player.MountedCenter - new Vector2((float)(5 * player.direction), 2f);
		if (AfterImageLength > 0)
		{
			oldProjectileRot.Add(base.Projectile.rotation);
			oldProjectilePos.Add(base.Projectile.Center + new Vector2(0f, base.Projectile.gfxOffY));
			if (oldProjectileRot.Count > AfterImageLength)
			{
				oldProjectileRot.RemoveAt(0);
				oldProjectilePos.RemoveAt(0);
			}
		}
		if (inSwing && swingTimer == 1 && UseSound.HasValue)
		{
			SoundEngine.PlaySound(UseSound.Value, player.Center);
		}
		float angle2 = ((AlternateSwings && modplayer.swingNum % 2 == 1) ? SwingFunction() : SwingFunction());
		base.Projectile.Center = armCenter - (angle * (float)OffsetDistance * (1f + (base.Projectile.scale - 1f) * 0.75f)).RotatedBy((float)base.Projectile.spriteDirection * angle2);
		base.Projectile.rotation = angle.RotatedBy((float)base.Projectile.spriteDirection * angle2).ToRotation() + adust;
		AdditionalAI();
		if (base.Projectile.active)
		{
			oldPlayerOffset = base.Projectile.Center - player.MountedCenter;
			player.itemTime = ExistsTime + 2 - timer;
			player.itemAnimation = ExistsTime + 2 - timer;
			if (timer > ExistsTime)
			{
				player.itemTime = 0;
				player.itemAnimation = 0;
				base.Projectile.Kill();
			}
			timer++;
			if (timer >= StartupTime && timer < StartupTime + swingTime)
			{
				swingTimer++;
			}
			Vector2 armDir = armCenter - base.Projectile.Center;
			armDir.Y *= player.gravDir;
			player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, armDir.ToRotation() + MathHelper.ToRadians(90f));
			oldScale.Insert(0, base.Projectile.scale);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		Main.player[base.Projectile.owner].GetModPlayer<BaseSwordHoldoutPlayer>();
		if (AfterImageLength > 0)
		{
			Texture2D texture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
			for (int i = 0; i < oldProjectileRot.Count; i++)
			{
				float col = base.Projectile.Opacity * ((float)i / (float)AfterImageLength) * 0.1f;
				if (base.Projectile.spriteDirection == 1)
				{
					Main.EntitySpriteDraw(texture, oldProjectilePos[i] - Main.screenPosition, null, AfterImageColor * col, oldProjectileRot[i], texture.Size() / 2f, oldScale[i], (SpriteEffects)0);
				}
				else
				{
					Main.EntitySpriteDraw(texture, oldProjectilePos[i] - Main.screenPosition, null, AfterImageColor * col, oldProjectileRot[i], texture.Size() / 2f, oldScale[i], (SpriteEffects)1);
				}
			}
		}
		if (drawSwordTrail && timer >= StartupTime && timer <= StartupTime + swingTime)
		{
			Main.spriteBatch.EnterShaderRegion();
			if (TrailTexture == null)
			{
				TrailTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/VoronoiShapes", (AssetRequestMode)2);
			}
			Vector2 trailOffset = (base.Projectile.rotation - (float)Math.PI / 4f).ToRotationVector2() + base.Projectile.Size * 0.5f;
			GameShaders.Misc["CalamityMod:ExobladeSlash"].SetShaderTexture(TrailTexture);
			GameShaders.Misc["CalamityMod:ExobladeSlash"].UseColor(trailColors[0]);
			GameShaders.Misc["CalamityMod:ExobladeSlash"].UseSecondaryColor(trailColors[1]);
			GameShaders.Misc["CalamityMod:ExobladeSlash"].Shader.Parameters["fireColor"].SetValue(((Color)(ref trailColors[2])).ToVector3());
			GameShaders.Misc["CalamityMod:ExobladeSlash"].Shader.Parameters["flipped"].SetValue(base.Projectile.spriteDirection != -1);
			GameShaders.Misc["CalamityMod:ExobladeSlash"].Apply();
			Vector2[] positionsToUse = base.Projectile.oldPos.Take((int)MathHelper.Min((float)trailLength, (float)swingTimer)).ToArray();
			for (int j = 0; j < positionsToUse.Length && j < timer; j++)
			{
				ref Vector2 reference = ref positionsToUse[j];
				reference += (base.Projectile.oldRot[j] - (float)Math.PI / 4f * (float)((base.Projectile.spriteDirection != -1) ? 1 : 3)).ToRotationVector2() * this.trailOffset * oldScale[j];
			}
			PrimitiveRenderer.RenderTrail(positionsToUse, new PrimitiveSettings(trailWidth, trailColor, delegate
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				return trailOffset;
			}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ExobladeSlash"]), 25);
			Main.spriteBatch.ExitShaderRegion();
			Main.player[base.Projectile.owner].heldProj = base.Projectile.whoAmI;
		}
		Main.player[base.Projectile.owner].heldProj = base.Projectile.whoAmI;
		return true;
	}

	public override void ModifyDamageHitbox(ref Rectangle hitbox)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = ((Rectangle)(ref hitbox)).Center.ToVector2();
		hitbox.Height = (int)((float)base.Projectile.height * base.Projectile.scale);
		hitbox.Width = (int)((float)base.Projectile.width * base.Projectile.scale);
		((Rectangle)(ref hitbox)).Location = (center - new Vector2((float)(hitbox.Width / 2), (float)(hitbox.Height / 2))).ToPoint();
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		if (lineCollisionLength > 0f)
		{
			Player player = Main.player[base.Projectile.owner];
			Vector2 swordDir = (player.MountedCenter - new Vector2((float)(5 * player.direction), 2f)).DirectionTo(base.Projectile.Center);
			Vector2 collisionline = Utils.RotatedBy(new Vector2(lineCollisionLength / 2f, 0f), (double)swordDir.ToRotation(), default(Vector2)) * base.Projectile.scale;
			if (Collision.CheckAABBvLineCollision(((Rectangle)(ref targetHitbox)).Location.ToVector2(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + collisionline) && !float.IsNaN(collisionline.X) && !float.IsNaN(collisionline.Y))
			{
				return true;
			}
		}
		return base.Colliding(projHitbox, targetHitbox);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		modifiers.HitDirectionOverride = ((Main.player[base.Projectile.owner].DirectionTo(target.Center).X >= 0f) ? 1 : (-1));
	}

	public override bool? CanDamage()
	{
		return inSwing;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		writer.WriteVector2(angle);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		angle = reader.ReadVector2();
	}

	public void shootCheck(int type = 0, float velocity = 1f, float damagemod = 1f, int amount = 0, int negate = 0, int ai0 = 0)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		if (negate == 0)
		{
			negate = base.Projectile.spriteDirection;
		}
		if (amount == 0)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, (base.Projectile.rotation + (float)negate * MathHelper.ToRadians((float)(45 - negate * 90))).ToRotationVector2() * velocity, type, (int)((float)base.Projectile.damage * damagemod), base.Projectile.knockBack, base.Projectile.owner, ai0);
			return;
		}
		amount++;
		if (swingTimer % (swingTime / amount) == 0 && swingTimer > 0 && swingTimer < swingTime - swingTime / amount / 2)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, (base.Projectile.rotation + (float)negate * MathHelper.ToRadians((float)(45 - negate * 90))).ToRotationVector2() * velocity, type, (int)((float)base.Projectile.damage * damagemod), base.Projectile.knockBack, base.Projectile.owner, ai0);
		}
	}

	protected BaseSwordHoldoutProjectile()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		swingWidth = 180;
		swingTime = 20;
		AlternateSwings = true;
		UsesBaseItem = true;
		useMeleeSpeed = true;
		useMeleeSize = true;
		trailColors = (Color[])(object)new Color[3]
		{
			Color.White,
			Color.Black,
			Color.Green
		};
		trailOffset = 25f;
		trailLength = 25;
		RotateInStartup = 0.5f;
		RotateInCooldown = 0.5f;
		AfterImageColor = Color.White;
		angle = Vector2.Zero;
		oldScale = new List<float>();
		oldProjectileRot = new List<float>();
		oldProjectilePos = new List<Vector2>();
		ExistsTime = 20;
		base._002Ector();
	}
}
