using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Systems.Mechanic;

public class ArenaWallPlayer : ModPlayer
{
	public Vector2 touchingSides;

	public override void PreUpdateMovement()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		foreach (ArenaWallSystem.Box box in ArenaWallSystem.ActiveBoxes)
		{
			if (box.ShouldEffectPlayer(base.Player) && box.Contains(base.Player.position, base.Player.Size))
			{
				ContainPlayerLogic(box);
			}
		}
	}

	private void ContainPlayerLogic(ArenaWallSystem.Box box)
	{
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0662: Unknown result type (might be due to invalid IL or missing references)
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_0668: Unknown result type (might be due to invalid IL or missing references)
		//IL_066d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0604: Unknown result type (might be due to invalid IL or missing references)
		//IL_060e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		//IL_0631: Unknown result type (might be due to invalid IL or missing references)
		//IL_063c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bc: Unknown result type (might be due to invalid IL or missing references)
		if (box.oldData != null && box.PullPlayerWhenSizeChanged)
		{
			if (touchingSides.X > 0f)
			{
				base.Player.position.Y += (box.oldData.TopLeft.Y - box.oldData.BottomRight.Y - (box.TopLeft.Y - box.BottomRight.Y)) * touchingSides.X;
			}
			if (touchingSides.Y > 0f)
			{
				base.Player.position.X += (box.oldData.TopLeft.X - box.oldData.BottomRight.X - (box.TopLeft.X - box.BottomRight.X)) * touchingSides.Y;
			}
		}
		touchingSides = Vector2.Zero;
		if (base.Player.Left.X < box.TopLeft.X)
		{
			base.Player.position.X = box.TopLeft.X;
			touchingSides.X = Utils.Remap(base.Player.Center.Y, box.TopLeft.Y, box.BottomRight.Y, 0f, 1f);
			touchingSides.Y = Utils.Remap(base.Player.Center.X, box.TopLeft.X, box.BottomRight.X, 0f, 1f);
		}
		if (base.Player.Right.X > box.BottomRight.X)
		{
			base.Player.position.X = box.BottomRight.X - (float)base.Player.width;
			touchingSides.X = Utils.Remap(base.Player.Center.Y, box.TopLeft.Y, box.BottomRight.Y, 0f, 1f);
			touchingSides.Y = Utils.Remap(base.Player.Center.X, box.TopLeft.X, box.BottomRight.X, 0f, 1f);
		}
		if (base.Player.TopLeft.Y < box.TopLeft.Y)
		{
			base.Player.position.Y = box.TopLeft.Y;
			touchingSides.X = Utils.Remap(base.Player.Center.Y, box.TopLeft.Y, box.BottomRight.Y, 0f, 1f);
			touchingSides.Y = Utils.Remap(base.Player.Center.X, box.TopLeft.X, box.BottomRight.X, 0f, 1f);
		}
		if (base.Player.BottomRight.Y > box.BottomRight.Y)
		{
			base.Player.position.Y = box.BottomRight.Y - (float)base.Player.height;
			touchingSides.X = Utils.Remap(base.Player.Center.Y, box.TopLeft.Y, box.BottomRight.Y, 0f, 1f);
			touchingSides.Y = Utils.Remap(base.Player.Center.X, box.TopLeft.X, box.BottomRight.X, 0f, 1f);
		}
		Vector2 originalVelocity = base.Player.velocity;
		Vector2 originalTopLeft = base.Player.TopLeft;
		Vector2 originalBottomRight = base.Player.BottomRight;
		Player player = base.Player;
		player.position += originalVelocity;
		if (base.Player.Left.X < box.TopLeft.X)
		{
			base.Player.velocity.X = box.TopLeft.X - originalTopLeft.X;
			if (base.Player.controlLeft)
			{
				base.Player.slideDir = -1;
				applyShoeSpikes();
			}
		}
		if (base.Player.Right.X > box.BottomRight.X)
		{
			base.Player.velocity.X = box.BottomRight.X - originalBottomRight.X;
			Dust.NewDustPerfect(base.Player.Right, 31);
			if (base.Player.controlRight)
			{
				base.Player.slideDir = 1;
				applyShoeSpikes();
			}
		}
		if (base.Player.TopLeft.Y < box.TopLeft.Y)
		{
			base.Player.velocity.Y = box.TopLeft.Y - originalTopLeft.Y;
			if (base.Player.velocity.Y == 0f)
			{
				base.Player.velocity.Y += 0.001f;
			}
			touchingSides.Y = Utils.Remap(base.Player.Center.X, box.TopLeft.X, box.BottomRight.X, 0f, 1f);
		}
		if (base.Player.BottomRight.Y > box.BottomRight.Y)
		{
			base.Player.velocity.Y = box.BottomRight.Y - originalBottomRight.Y;
			touchingSides.Y = Utils.Remap(base.Player.Center.X, box.TopLeft.X, box.BottomRight.X, 0f, 1f);
		}
		Player player2 = base.Player;
		player2.position -= originalVelocity;
		void applyShoeSpikes()
		{
			if (base.Player.spikedBoots != 0 && !(base.Player.velocity.Y < 0f) && !base.Player.mount.Active)
			{
				if (base.Player.controlDown && base.Player.spikedBoots > 0)
				{
					base.Player.velocity.Y = 4f * base.Player.gravDir;
					spawnShoeDust();
				}
				else if (base.Player.spikedBoots <= 2)
				{
					base.Player.velocity.Y = 0f;
				}
				else if (base.Player.spikedBoots == 1)
				{
					base.Player.velocity.Y = 0.5f * base.Player.gravDir;
					spawnShoeDust();
				}
			}
		}
		void spawnShoeDust()
		{
			//IL_0078: Unknown result type (might be due to invalid IL or missing references)
			//IL_008e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0094: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0106: Unknown result type (might be due to invalid IL or missing references)
			//IL_010b: Unknown result type (might be due to invalid IL or missing references)
			int num4 = Dust.NewDust(new Vector2(base.Player.position.X + (float)(base.Player.width / 2) + (float)((base.Player.width / 2 - 4) * base.Player.slideDir), base.Player.position.Y + (float)(base.Player.height / 2) + (float)(base.Player.height / 2 - 4) * base.Player.gravDir), 8, 8, 31);
			if (base.Player.slideDir < 0)
			{
				Main.dust[num4].position.X -= 10f;
			}
			if (base.Player.gravDir < 0f)
			{
				Main.dust[num4].position.Y -= 12f;
			}
			Dust obj = Main.dust[num4];
			obj.velocity *= 0.1f;
			Main.dust[num4].scale *= 1.2f;
			Main.dust[num4].noGravity = true;
			Main.dust[num4].shader = GameShaders.Armor.GetSecondaryShader(base.Player.cShoe, base.Player);
		}
	}
}
