using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.BaseProjectiles;

public abstract class BaseGunHoldoutProjectile : ModProjectile, ILocalizedModType, IModType
{
	public abstract int AssociatedItemID { get; }

	public virtual Vector2 GunTipPosition
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Center + Vector2.UnitX.RotatedBy(base.Projectile.rotation) * (float)base.Projectile.width * 0.5f;
		}
	}

	public virtual float WeaponTurnSpeed => 0.2f;

	public virtual float RecoilResolveSpeed => 0.3f;

	public virtual float MaxOffsetLengthFromArm { get; }

	public virtual float OffsetXUpwards { get; }

	public virtual float OffsetXDownwards { get; }

	public virtual float BaseOffsetY { get; }

	public virtual float OffsetYUpwards { get; }

	public virtual float OffsetYDownwards { get; }

	public Player Owner { get; private set; }

	public Item HeldItem { get; private set; }

	public bool KeepRefreshingLifetime { get; set; } = true;

	public bool SetUsage { get; set; } = true;

	public float OffsetLengthFromArm { get; set; }

	public float ExtraFrontArmRotation { get; set; }

	public float ExtraBackArmRotation { get; set; }

	public Player.CompositeArmStretchAmount FrontArmStretch { get; set; }

	public Player.CompositeArmStretchAmount BackArmStretch { get; set; }

	private Type AssociatedItemType => ItemLoader.GetItem(AssociatedItemID).GetType();

	private Asset<Texture2D> ItemTexture => TextureAssets.Item[AssociatedItemID];

	public override LocalizedText DisplayName => CalamityUtils.GetItemName(AssociatedItemID);

	public override string Texture => (AssociatedItemType.Namespace + "." + AssociatedItemType.Name).Replace('.', '/');

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = ((ItemTexture == null) ? 1 : ItemTexture.Width()));
		base.Projectile.tileCollide = false;
		base.Projectile.netImportant = true;
	}

	public override void OnSpawn(IEntitySource source)
	{
		OffsetLengthFromArm = MaxOffsetLengthFromArm;
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void AI()
	{
		if (Owner == null)
		{
			Player player = (Owner = Main.player[base.Projectile.owner]);
		}
		if (HeldItem == null)
		{
			Item item = (HeldItem = Owner.HeldItem);
		}
		KillHoldoutLogic();
		ManageHoldout();
		HoldoutAI();
	}

	public virtual void KillHoldoutLogic()
	{
		if (Owner.CantUseHoldout())
		{
			base.Projectile.Kill();
		}
	}

	public virtual void ManageHoldout()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		Vector2 armPosition = Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true);
		float holdoutDirection = base.Projectile.velocity.ToRotation();
		if (base.Projectile.owner == Main.myPlayer)
		{
			Vector2 ownerToMouse = Owner.Calamity().mouseWorld - armPosition;
			float proximityLookingUpwards = Vector2.Dot(ownerToMouse.SafeNormalize(Vector2.Zero), -Vector2.UnitY * Owner.gravDir);
			int direction = MathF.Sign(ownerToMouse.X);
			Vector2 lengthOffset = base.Projectile.rotation.ToRotationVector2() * OffsetLengthFromArm;
			Vector2 armOffset = default(Vector2);
			((Vector2)(ref armOffset))._002Ector(Utils.Remap(MathF.Abs(proximityLookingUpwards), 0f, 1f, 0f, (proximityLookingUpwards > 0f) ? OffsetXUpwards : OffsetXDownwards) * (float)direction, BaseOffsetY * Owner.gravDir + Utils.Remap(MathF.Abs(proximityLookingUpwards), 0f, 1f, 0f, (proximityLookingUpwards > 0f) ? OffsetYUpwards : OffsetYDownwards) * Owner.gravDir);
			base.Projectile.Center = armPosition + lengthOffset + armOffset;
			base.Projectile.velocity = holdoutDirection.AngleTowards(ownerToMouse.ToRotation(), WeaponTurnSpeed).ToRotationVector2();
			base.Projectile.rotation = holdoutDirection;
			base.Projectile.spriteDirection = direction;
			Owner.ChangeDir(direction);
		}
		else
		{
			Vector2 lengthOffset2 = base.Projectile.rotation.ToRotationVector2() * OffsetLengthFromArm;
			float proximityLookingUpwards2 = Vector2.Dot(base.Projectile.velocity.SafeNormalize(Vector2.Zero), -Vector2.UnitY * Owner.gravDir);
			int direction2 = base.Projectile.spriteDirection;
			Vector2 armOffset2 = default(Vector2);
			((Vector2)(ref armOffset2))._002Ector(Utils.Remap(MathF.Abs(proximityLookingUpwards2), 0f, 1f, 0f, (proximityLookingUpwards2 > 0f) ? OffsetXUpwards : OffsetXDownwards) * (float)direction2, BaseOffsetY * Owner.gravDir + Utils.Remap(MathF.Abs(proximityLookingUpwards2), 0f, 1f, 0f, (proximityLookingUpwards2 > 0f) ? OffsetYUpwards : OffsetYDownwards) * Owner.gravDir);
			base.Projectile.Center = armPosition + lengthOffset2 + armOffset2;
			base.Projectile.velocity = base.Projectile.rotation.ToRotationVector2();
			Owner.ChangeDir(direction2);
		}
		int currentDirection = base.Projectile.spriteDirection;
		Owner.heldProj = base.Projectile.whoAmI;
		if (SetUsage)
		{
			Owner.itemTime = (Owner.itemAnimation = 2);
		}
		Owner.itemRotation = (base.Projectile.velocity * (float)base.Projectile.direction).ToRotation();
		float armRotation = (base.Projectile.rotation - (float)Math.PI / 2f) * Owner.gravDir + ((Owner.gravDir == -1f) ? ((float)Math.PI) : 0f);
		Owner.SetCompositeArmFront(enabled: true, FrontArmStretch, armRotation + ExtraFrontArmRotation * (float)currentDirection);
		Owner.SetCompositeArmBack(enabled: true, BackArmStretch, armRotation + ExtraBackArmRotation * (float)currentDirection);
		if (KeepRefreshingLifetime)
		{
			base.Projectile.timeLeft = 2;
		}
		if (OffsetLengthFromArm != MaxOffsetLengthFromArm)
		{
			OffsetLengthFromArm = MathHelper.Lerp(OffsetLengthFromArm, MaxOffsetLengthFromArm, RecoilResolveSpeed);
		}
		if (base.Projectile.owner == Main.myPlayer)
		{
			base.Projectile.ForceNetUpdate();
		}
	}

	public abstract void HoldoutAI();

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Vector2 rotationPoint = texture.Size() * 0.5f;
		float drawRotation = base.Projectile.rotation;
		SpriteEffects flipSprite = (SpriteEffects)0;
		if (Owner.gravDir == 1f)
		{
			if (base.Projectile.spriteDirection == -1)
			{
				flipSprite = (SpriteEffects)2;
			}
		}
		else
		{
			rotationPoint.Y = (float)texture.Height - rotationPoint.Y;
			if (base.Projectile.spriteDirection == 1)
			{
				flipSprite = (SpriteEffects)2;
			}
		}
		Main.EntitySpriteDraw(texture, drawPosition, null, base.Projectile.GetAlpha(lightColor), drawRotation, rotationPoint, base.Projectile.scale, flipSprite);
		return false;
	}

	public sealed override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.rotation);
		writer.Write(KeepRefreshingLifetime);
		writer.Write(OffsetLengthFromArm);
		writer.Write7BitEncodedInt(base.Projectile.spriteDirection);
		SendExtraAIHoldout(writer);
	}

	public sealed override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.rotation = reader.ReadSingle();
		KeepRefreshingLifetime = reader.ReadBoolean();
		OffsetLengthFromArm = reader.ReadSingle();
		base.Projectile.spriteDirection = reader.Read7BitEncodedInt();
		ReceiveExtraAIHoldout(reader);
	}

	public virtual void SendExtraAIHoldout(BinaryWriter writer)
	{
	}

	public virtual void ReceiveExtraAIHoldout(BinaryReader reader)
	{
	}
}
