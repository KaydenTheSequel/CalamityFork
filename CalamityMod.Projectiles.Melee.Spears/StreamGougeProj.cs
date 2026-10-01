using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Spears;

public class StreamGougeProj : ModProjectile
{
	public int Time;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<StreamGouge>();

	public Player Owner => Main.player[base.Projectile.owner];

	public float SpinCompletion => Utils.GetLerpValue(0f, 45f, Time, clamped: true);

	public ref float InitialDirection => ref base.Projectile.ai[0];

	public ref float SpinDirection => ref base.Projectile.ai[1];

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 40;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.hide = true;
		base.Projectile.timeLeft = 90000;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = base.Projectile.MaxUpdates * 13;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(Time);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		Time = reader.ReadInt32();
	}

	public override void AI()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		if ((float)Time == 0f)
		{
			SoundEngine.PlaySound(in CommonCalamitySounds.MeatySlashSound, base.Projectile.Center);
		}
		if (InitialDirection == 0f)
		{
			InitialDirection = base.Projectile.velocity.ToRotation();
			SpinDirection = Main.rand.NextBool().ToDirectionInt();
			base.Projectile.netUpdate = true;
		}
		else
		{
			float stabOffset = (float)Math.Sin((float)Time / 3f) * 15f;
			float spearReach = MathHelper.Lerp(-10f, stabOffset + 90f, Utils.GetLerpValue(0f, 24f, Time - 45, clamped: true));
			base.Projectile.velocity = InitialDirection.ToRotationVector2() * spearReach;
		}
		base.Projectile.rotation = (float)Math.Pow(SpinCompletion, 0.82) * (float)Math.PI * SpinDirection * 4f + InitialDirection - (float)Math.PI / 4f + (float)Math.PI;
		DeterminePlayerVariables();
		if (Main.myPlayer == base.Projectile.owner && Time >= 69 && (float)Time % 9f == 8f)
		{
			Vector2 val = Owner.ClampedMouseWorld();
			Vector2 portalSpawnPosition = val + Main.rand.NextVector2Unit() * Main.rand.NextFloat(50f, 140f);
			Vector2 spearVelocity = (val - portalSpawnPosition).SafeNormalize(Vector2.UnitY);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), portalSpawnPosition, spearVelocity, ModContent.ProjectileType<StreamGougePortal>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		}
		Time++;
	}

	public void DeterminePlayerVariables()
	{
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		Owner.ChangeDir((Math.Cos(base.Projectile.rotation - (float)Math.PI + (float)Math.PI / 4f) > 0.0).ToDirectionInt());
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = (Owner.itemAnimation = 2);
		Owner.itemRotation = CalamityUtils.WrapAngle90Degrees(MathHelper.WrapAngle(base.Projectile.rotation - (float)Math.PI + (float)Math.PI / 4f));
		base.Projectile.Center = Owner.Center;
		if (Owner.CantUseHoldout())
		{
			base.Projectile.Kill();
		}
	}

	public void DrawPortal(Vector2 drawPosition, float opacity)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/StreamGougePortal", (AssetRequestMode)2).Value;
		Vector2 origin = value.Size() * 0.5f;
		Color baseColor = Color.White;
		float rotation = Main.GlobalTimeWrappedHourly * 6f;
		Color color = Color.Lerp(baseColor, Color.Black, 0.55f).MultiplyRGB(Color.DarkGray) * opacity;
		Color color2 = color;
		((Color)(ref color2)).A = 0;
		Main.EntitySpriteDraw(value, drawPosition, null, color2, rotation, origin, base.Projectile.scale * 1.2f, (SpriteEffects)0);
		color2 = color;
		((Color)(ref color2)).A = 0;
		Main.EntitySpriteDraw(value, drawPosition, null, color2, 0f - rotation, origin, base.Projectile.scale * 1.2f, (SpriteEffects)0);
		color = Color.Lerp(baseColor, Color.Cyan, 0.55f) * opacity * 1.6f;
		color2 = color;
		((Color)(ref color2)).A = 0;
		Main.EntitySpriteDraw(value, drawPosition, null, color2, rotation * 0.6f, origin, base.Projectile.scale * 1.2f, (SpriteEffects)0);
		color = Color.Lerp(baseColor, Color.Fuchsia, 0.55f) * opacity * 1.6f;
		color2 = color;
		((Color)(ref color2)).A = 0;
		Main.EntitySpriteDraw(value, drawPosition, null, color2, rotation * -0.6f, origin, base.Projectile.scale * 1.2f, (SpriteEffects)0);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		if (SpinCompletion >= 0f && SpinCompletion < 1f)
		{
			Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Particles/SemiCircularSmear", (AssetRequestMode)2).Value;
			float rotation = base.Projectile.rotation - (float)Math.PI / 5f;
			if (SpinDirection == -1f)
			{
				rotation += (float)Math.PI;
			}
			Color smearColor = Color.Fuchsia * CalamityUtils.Convert01To010(SpinCompletion) * 0.9f;
			Vector2 smearOrigin = value.Size() * 0.5f;
			Vector2 position = Owner.Center - Main.screenPosition;
			Color color = smearColor;
			((Color)(ref color)).A = 0;
			Main.EntitySpriteDraw(value, position, null, color, rotation, smearOrigin, base.Projectile.scale * 1.45f, (SpriteEffects)0);
			Main.spriteBatch.ExitShaderRegion();
		}
		float portalOpacity = Utils.GetLerpValue(0.6f, 1f, SpinCompletion, clamped: true);
		bool num = portalOpacity >= 1f;
		Vector2 portalDrawPosition = Owner.Center + InitialDirection.ToRotationVector2() * 130f - Main.screenPosition;
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		if (num)
		{
			Main.spriteBatch.EnterShaderRegion();
			Vector2 intersectionNormal = portalDrawPosition + Main.screenPosition - base.Projectile.Center;
			Vector2 worldOffset = base.Projectile.rotation.ToRotationVector2() * Utils.GetLerpValue(0f, 24f, Time - 45, clamped: true) * 80f;
			Vector2 intersectionOffset = (portalDrawPosition + Main.screenPosition - base.Projectile.Center) * -1.5f;
			GameShaders.Misc["CalamityMod:IntersectionClip"].Shader.Parameters["uIntersectionPosition"].SetValue(portalDrawPosition + intersectionOffset);
			GameShaders.Misc["CalamityMod:IntersectionClip"].Shader.Parameters["uIntersectionNormal"].SetValue(intersectionNormal);
			GameShaders.Misc["CalamityMod:IntersectionClip"].Shader.Parameters["uIntersectionCutoffDirection"].SetValue(1f);
			GameShaders.Misc["CalamityMod:IntersectionClip"].Shader.Parameters["uWorldPosition"].SetValue(base.Projectile.Center - Main.screenPosition + worldOffset);
			GameShaders.Misc["CalamityMod:IntersectionClip"].Shader.Parameters["uSize"].SetValue(texture.Size());
			GameShaders.Misc["CalamityMod:IntersectionClip"].Shader.Parameters["uRotation"].SetValue(base.Projectile.rotation);
			GameShaders.Misc["CalamityMod:IntersectionClip"].Apply();
		}
		Main.EntitySpriteDraw(position: base.Projectile.Center - Main.screenPosition, origin: texture.Size() * 0.5f, texture: texture, sourceRectangle: null, color: base.Projectile.GetAlpha(lightColor), rotation: base.Projectile.rotation, scale: 1f, effects: (SpriteEffects)0);
		if (num)
		{
			Main.spriteBatch.ExitShaderRegion();
		}
		DrawPortal(portalDrawPosition, portalOpacity);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 300);
	}
}
