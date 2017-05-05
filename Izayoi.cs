using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.DirectX;
using Direct3D = Microsoft.DirectX.Direct3D;

namespace Izayoi {
	class Izayoi : Form {

		int width = 1800;
		int height = 900;
		float angle = 0;
		Direct3D.Device DXDevice = null;
		static Direct3D.CustomVertex.PositionColored[] vertices = null;
		Direct3D.VertexBuffer vb = null;
		Direct3D.IndexBuffer ib = null;
		int[] indexes;
		bool wireFrame = false;

		public Izayoi() {
			this.ClientSize = new Size(width, height);
			this.Text = "Izayoi";
		}
		public static void Main() {
			Izayoi form = new Izayoi();

			if (form.IniDirect3D() == false)
				return;

			form.Draw();
			form.Indexes();
			form.Show();
			while (form.Created) {
				form.Camera();
				form.Render();
				Application.DoEvents();
			}
		}
		public void Camera() {
			DXDevice.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4, 1, 0, int.MaxValue);
			DXDevice.Transform.View = Matrix.LookAtLH(new Vector3(10, 35, -100), new Vector3(10, 35, 0), new Vector3(-0.5f, 0.5f, 0));
			//DXDevice.Transform.View = Matrix.LookAtLH(new Vector3(0, 100, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 1));
		}
		public void Render() {
			if (DXDevice == null)
				return;

			DXDevice.Clear(Direct3D.ClearFlags.Target, Color.Gray, 1, 0);
			DXDevice.BeginScene();
			DXDevice.VertexFormat = Direct3D.CustomVertex.PositionColored.Format;
			DXDevice.SetStreamSource(0, vb, 0);
			DXDevice.Indices = ib;
			DXDevice.Transform.World = Matrix.RotationY(angle);
			DXDevice.DrawIndexedPrimitives(Direct3D.PrimitiveType.TriangleList, 0, 0, vertices.Length, 0, indexes.Length/3);
			DXDevice.EndScene();
			DXDevice.Present();
			angle += 0.02f;
		}
		private void LowestPartVertexes(int startVertex) {

			vertices[startVertex + 0].Position = new Vector3(0, 0, -1);
			vertices[startVertex + 1].Position = new Vector3(0, 2.5f, -1);
			vertices[startVertex + 2].Position = new Vector3(1, 1.75f, -1);
			vertices[startVertex + 3].Position = new Vector3(1.5f, 0, -1);
			vertices[startVertex + 4].Position = new Vector3(1, -1.75f, -1);
			vertices[startVertex + 5].Position = new Vector3(0, -2.5f, -1);
			vertices[startVertex + 6].Position = new Vector3(-1, -1.75f, -1);
			vertices[startVertex + 7].Position = new Vector3(-1.5f, 0, -1);
			vertices[startVertex + 8].Position = new Vector3(-1, 1.75f, -1);
			vertices[startVertex + 9].Position = new Vector3(0, 0, 1);
			vertices[startVertex + 10].Position = new Vector3(0, 2.5f, 1);
			vertices[startVertex + 11].Position = new Vector3(1, 1.75f, 1);
			vertices[startVertex + 12].Position = new Vector3(1.5f, 0, 1);
			vertices[startVertex + 13].Position = new Vector3(1, -1.75f, 1);
			vertices[startVertex + 14].Position = new Vector3(0, -2.5f, 1);
			vertices[startVertex + 15].Position = new Vector3(-1, -1.75f, 1);
			vertices[startVertex + 16].Position = new Vector3(-1.5f, 0, 1);
			vertices[startVertex + 17].Position = new Vector3(-1, 1.75f, 1);

			vertices[startVertex + 18].Position = new Vector3(0, 0, -1);
			vertices[startVertex + 19].Position = new Vector3(0, 2.5f * .7f, -1);
			vertices[startVertex + 20].Position = new Vector3(1 * .7f, 1.75f * .7f, -1);
			vertices[startVertex + 21].Position = new Vector3(1.5f * .7f, 0, -1);
			vertices[startVertex + 22].Position = new Vector3(1f * .7f, -1.75f * .7f, -1);
			vertices[startVertex + 23].Position = new Vector3(0, -2.5f * .7f, -1);
			vertices[startVertex + 24].Position = new Vector3(-1 * .7f, -1.75f * .7f, -1);
			vertices[startVertex + 25].Position = new Vector3(-1.5f * .7f, 0, -1);
			vertices[startVertex + 26].Position = new Vector3(-1 * .7f, 1.75f * .7f, -1);
			vertices[startVertex + 27].Position = new Vector3(0, 0, 1);
			vertices[startVertex + 28].Position = new Vector3(0, 2.5f * .7f, 1);
			vertices[startVertex + 29].Position = new Vector3(1 * .7f, 1.75f * .7f, 1);
			vertices[startVertex + 30].Position = new Vector3(1.5f * .7f, 0, 1);
			vertices[startVertex + 31].Position = new Vector3(1 * .7f, -1.75f * .7f, 1);
			vertices[startVertex + 32].Position = new Vector3(0, -2.5f * .7f, 1);
			vertices[startVertex + 33].Position = new Vector3(-1 * .7f, -1.75f * .7f, 1);
			vertices[startVertex + 34].Position = new Vector3(-1.5f * .7f, 0, 1);
			vertices[startVertex + 35].Position = new Vector3(-1 * .7f, 1.75f * .7f, 1);

			for (int i = 0; i < 18; i++)
				vertices[i].Color = Color.Yellow.ToArgb();
			for (int i = 18; i < 36; i++)
				vertices[i].Color = Color.White.ToArgb();

		}
		private void OctagonalPrism(int startVertex, float factor, float height) {

			vertices[startVertex + 0].Position = new Vector3(0, height, 0.75f * factor);
			vertices[startVertex + 1].Position = new Vector3(0.7f * factor, height, 0.5f * factor);
			vertices[startVertex + 2].Position = new Vector3(1 * factor, height, 0);
			vertices[startVertex + 3].Position = new Vector3(0.7f * factor, height, -0.5f * factor);
			vertices[startVertex + 4].Position = new Vector3(0, height, -0.75f * factor);
			vertices[startVertex + 5].Position = new Vector3(-0.7f * factor, height, -0.5f * factor);
			vertices[startVertex + 6].Position = new Vector3(-1 * factor, height, 0);
			vertices[startVertex + 7].Position = new Vector3(-0.7f * factor, height, 0.5f * factor);
		}
		private void HiltVertexes(int startVertex) {
			
			int c = 0;
			float factor = 0.75f;
			float height = 1.75f;

			OctagonalPrism(startVertex + 8 * c, factor, height);

			factor = 0.9f;
			height = 9;
			c++;

			OctagonalPrism(startVertex + 8 * c, factor, height);

			factor = 1.05f;
			height = 15;
			c++;

			OctagonalPrism(startVertex + 8 * c, factor, height);

			factor = 1.3f;
			height = 20;
			c++;

			OctagonalPrism(startVertex + 8 * c, factor, height);

			factor = 1.75f;
			height = 23;
			c++;

			OctagonalPrism(startVertex + 8 * c, factor, height);

			factor = 2.25f;
			height = 25;
			c++;

			OctagonalPrism(startVertex + 8 * c, factor, height);

			float factorZ = 2f;
			factor = 3;
			height = 28;
			c++;

			vertices[startVertex + 0 + 8 * c].Position = new Vector3(0, height - 2, 0.75f * factorZ);
			vertices[startVertex + 1 + 8 * c].Position = new Vector3(0.7f * factor, height - 1.2f, 0.5f * factorZ);
			vertices[startVertex + 2 + 8 * c].Position = new Vector3(1 * factor, height, 0);
			vertices[startVertex + 3 + 8 * c].Position = new Vector3(0.7f * factor, height - 1.2f, -0.5f * factorZ);
			vertices[startVertex + 4 + 8 * c].Position = new Vector3(0, height - 2, -0.75f * factorZ);
			vertices[startVertex + 5 + 8 * c].Position = new Vector3(-0.7f * factor, height - 1.2f, -0.5f * factorZ);
			vertices[startVertex + 6 + 8 * c].Position = new Vector3(-1 * factor, height, 0);
			vertices[startVertex + 7 + 8 * c].Position = new Vector3(-0.7f * factor, height - 1.2f, 0.5f * factorZ);

			c++;

			for (int i = startVertex; i < startVertex + 8 * (c - 4); i++)
				vertices[i].Color = Color.Yellow.ToArgb();
			for (int i = startVertex + 8 * (c - 4); i < startVertex + 8 * (c - 1); i++)
				vertices[i].Color = Color.Gold.ToArgb();

			factorZ = 1;
			factor = 2;
			
			vertices[startVertex + 0 + 8 * c].Position = new Vector3(0, height - 1.6f, 0.75f * factorZ);
			vertices[startVertex + 1 + 8 * c].Position = new Vector3(0.7f * factor, height - 1, 0.5f * factorZ);
			vertices[startVertex + 2 + 8 * c].Position = new Vector3(1 * factor, height, 0);
			vertices[startVertex + 3 + 8 * c].Position = new Vector3(0.7f * factor, height - 1, -0.5f * factorZ);
			vertices[startVertex + 4 + 8 * c].Position = new Vector3(0, height - 1.6f, -0.75f * factorZ);
			vertices[startVertex + 5 + 8 * c].Position = new Vector3(-0.7f * factor, height - 1, -0.5f * factorZ);
			vertices[startVertex + 6 + 8 * c].Position = new Vector3(-1 * factor, height, 0);
			vertices[startVertex + 7 + 8 * c].Position = new Vector3(-0.7f * factor, height - 1, 0.5f * factorZ);

			c++;

			for (int i = startVertex + 8 * (c - 2); i < startVertex + 8 * c; i++)
				vertices[i].Color = Color.Orange.ToArgb();
		}
		private void BladePrism(int startVertex, float factor, float height, float zFactor) {

			vertices[startVertex + 0].Position = new Vector3(0, height, 0.5f * zFactor);
			vertices[startVertex + 1].Position = new Vector3(0.7f * factor, height, 0.3f * zFactor);
			vertices[startVertex + 2].Position = new Vector3(1 * factor, height, 0);
			vertices[startVertex + 3].Position = new Vector3(0.7f * factor, height, -0.3f * zFactor);
			vertices[startVertex + 4].Position = new Vector3(0, height, -0.5f * zFactor);
			vertices[startVertex + 5].Position = new Vector3(-0.7f * factor, height, -0.3f * zFactor);
			vertices[startVertex + 6].Position = new Vector3(-1 * factor, height, 0);
			vertices[startVertex + 7].Position = new Vector3(-0.7f * factor, height, 0.3f * zFactor);
		}
		private void BladeVertexes(int startVertex) {

			int c = 0,
				length = 40;
			float factor = 2.5f,
				initialFactor = factor,
				height = 28,
				initialHeight = height,
				zFactor = 1;
			//Lower curve
			vertices[startVertex + 0 + 8 * c].Position = new Vector3(0, height - 1.6f, 0.75f * zFactor);
			vertices[startVertex + 1 + 8 * c].Position = new Vector3(0.7f * factor, height - 1, 0.5f * zFactor);
			vertices[startVertex + 2 + 8 * c].Position = new Vector3(1 * factor, height, 0);
			vertices[startVertex + 3 + 8 * c].Position = new Vector3(0.7f * factor, height - 1, -0.5f * zFactor);
			vertices[startVertex + 4 + 8 * c].Position = new Vector3(0, height - 1.6f, -0.75f * zFactor);
			vertices[startVertex + 5 + 8 * c].Position = new Vector3(-0.7f * factor, height - 1, -0.5f * zFactor);
			vertices[startVertex + 6 + 8 * c].Position = new Vector3(-1 * factor, height, 0);
			vertices[startVertex + 7 + 8 * c].Position = new Vector3(-0.7f * factor, height - 1, 0.5f * zFactor);
			c++;
			factor = initialFactor / (c + 1) + (float)(2 * Math.Sqrt(c));
			height = 28 + c;
			zFactor = 1.2f;
			BladePrism(startVertex + 8 * c, factor, height, zFactor);
			c++;
			factor = initialFactor / (c + 1) + (float)(2 * Math.Sqrt(c));
			height = 28 + c;
			zFactor = 1.25f;
			BladePrism(startVertex + 8 * c, factor, height, zFactor);
			c++;
			factor = initialFactor / (c + 1) + (float)(2 * Math.Sqrt(c));
			height = 28 + c;
			zFactor = 1.275f;
			BladePrism(startVertex + 8 * c, factor, height, zFactor);
			c++;
			factor = initialFactor / (c + 1) + (float)(2 * Math.Sqrt(c));
			height = 28 + c;
			zFactor = 1.28f;
			BladePrism(startVertex + 8 * c, factor, height, zFactor);
			
			//Mid curve
			initialHeight = height;
			int initialC = c;
			initialFactor = factor;
			c++;

			for (; c < 15; c++) {
				height += 1f / 2;
				factor = initialFactor - (2.5f - 0.1f * c) * (float)Math.Sqrt(height - initialHeight);
				factor *= 1.15f;
				zFactor += 0.05f;
				BladePrism(startVertex + 8 * c, factor, height, zFactor);
			}
			initialHeight = height;
			initialC = c;
			for (; c < 25; c++) {
				height += 1f / 2;
				factor = initialFactor - (2.5f - 0.1f * c) * (float)Math.Sqrt(5 + height - initialHeight);
				factor *= 1.15f + (0.05f * (c - initialC));
				zFactor -= 0.05f;
				BladePrism(startVertex + 8 * c, factor, height, zFactor);
			}
			initialHeight = height;
			initialFactor = factor;
			initialC = c;
			for (; c < 40; c++) {
				height += 28f / 15;
				factor = initialFactor - 0.00001f * (9 * (c - initialC)) * (height * height);
				factor *= 1f;
				zFactor -= 1.28f / (15 + (c - initialC));
				if (c == 39)
					zFactor -= 0.2f;
				BladePrism(startVertex + 8 * c, factor, height, zFactor);
			}
			BladePrism(startVertex + 8 * c, 0, height + 28f / 20, 0);
			c++;

			for (int i = startVertex; i < startVertex + 8 * c; i++) {
				if (i % 4 == 0)
					vertices[i].Color = Color.Firebrick.ToArgb();
				else {
					if (i % 2 == 0)
						vertices[i].Color = Color.Silver.ToArgb();
					else
						vertices[i].Color = Color.Red.ToArgb();
				}
			}
			for (int i = startVertex + 8 * (c - 1); i < startVertex + 8 * c; i++) {
				vertices[i].Color = Color.Silver.ToArgb();
			}
		}
		public void Draw() {
			vertices = new Direct3D.CustomVertex.PositionColored[700];
			LowestPartVertexes(0);
			//highest vertex is now 36
			HiltVertexes(36);
			//highest vertex is now 104
			BladeVertexes(104);
			//highes vertex should now be about 424

			vb = new Direct3D.VertexBuffer(typeof(Direct3D.CustomVertex.PositionColored), vertices.Length, DXDevice, Direct3D.Usage.Dynamic | Direct3D.Usage.WriteOnly, Direct3D.CustomVertex.PositionColored.Format, Direct3D.Pool.Default);
			vb.SetData(vertices, 0, Direct3D.LockFlags.None);
		}
		private void LowestPartIndexes(int startIndex) {
			indexes[startIndex+0] = 0;
			indexes[startIndex+1] = 1;
			indexes[startIndex+2] = 2;

			indexes[startIndex+3] = 0;
			indexes[startIndex+4] = 2;
			indexes[startIndex+5] = 3;

			indexes[startIndex+6] = 0;
			indexes[startIndex+7] = 3;
			indexes[startIndex+8] = 4;

			indexes[startIndex+9] = 0;
			indexes[startIndex+10] = 4;
			indexes[startIndex+11] = 5;

			indexes[startIndex+12] = 0;
			indexes[startIndex+13] = 5;
			indexes[startIndex+14] = 6;

			indexes[startIndex+15] = 0;
			indexes[startIndex+16] = 6;
			indexes[startIndex+17] = 7;

			indexes[startIndex+18] = 0;
			indexes[startIndex+19] = 7;
			indexes[startIndex+20] = 8;

			indexes[startIndex+21] = 0;
			indexes[startIndex+22] = 8;
			indexes[startIndex+23] = 1;

			indexes[startIndex+24] = 9;
			indexes[startIndex+25] = 11;
			indexes[startIndex+26] = 10;

			indexes[startIndex+27] = 9;
			indexes[startIndex+28] = 12;
			indexes[startIndex+29] = 11;

			indexes[startIndex+30] = 9;
			indexes[startIndex+31] = 13;
			indexes[startIndex+32] = 12;

			indexes[startIndex+33] = 9;
			indexes[startIndex+34] = 14;
			indexes[startIndex+35] = 13;

			indexes[startIndex+36] = 9;
			indexes[startIndex+37] = 15;
			indexes[startIndex+38] = 14;

			indexes[startIndex+39] = 9;
			indexes[startIndex+40] = 16;
			indexes[startIndex+41] = 15;

			indexes[startIndex+42] = 9;
			indexes[startIndex+43] = 17;
			indexes[startIndex+44] = 16;

			indexes[startIndex+45] = 9;
			indexes[startIndex+46] = 10;
			indexes[startIndex+47] = 17;

			indexes[startIndex+48] = 1;
			indexes[startIndex+49] = 10;
			indexes[startIndex+50] = 11;

			indexes[startIndex+51] = 11;
			indexes[startIndex+52] = 2;
			indexes[startIndex+53] = 1;

			indexes[startIndex+54] = 2;
			indexes[startIndex+55] = 11;
			indexes[startIndex+56] = 12;

			indexes[startIndex+57] = 12;
			indexes[startIndex+58] = 3;
			indexes[startIndex+59] = 2;

			indexes[startIndex+60] = 3;
			indexes[startIndex+61] = 12;
			indexes[startIndex+62] = 13;

			indexes[startIndex+63] = 13;
			indexes[startIndex+64] = 4;
			indexes[startIndex+65] = 3;

			indexes[startIndex+66] = 4;
			indexes[startIndex+67] = 13;
			indexes[startIndex+68] = 14;

			indexes[startIndex+69] = 14;
			indexes[startIndex+70] = 5;
			indexes[startIndex+71] = 4;

			indexes[startIndex+72] = 5;
			indexes[startIndex+73] = 14;
			indexes[startIndex+74] = 15;

			indexes[startIndex+75] = 15;
			indexes[startIndex+76] = 6;
			indexes[startIndex+77] = 5;

			indexes[startIndex+78] = 6;
			indexes[startIndex+79] = 15;
			indexes[startIndex+80] = 16;

			indexes[startIndex+81] = 16;
			indexes[startIndex+82] = 7;
			indexes[startIndex+83] = 6;

			indexes[startIndex+84] = 7;
			indexes[startIndex+85] = 16;
			indexes[startIndex+86] = 17;

			indexes[startIndex+87] = 17;
			indexes[startIndex+88] = 8;
			indexes[startIndex+89] = 7;

			indexes[startIndex+90] = 8;
			indexes[startIndex+91] = 17;
			indexes[startIndex+92] = 10;

			indexes[startIndex+93] = 10;
			indexes[startIndex+94] = 1;
			indexes[startIndex+95] = 8;

			indexes[startIndex+96] = 0;
			indexes[startIndex+97] = 1;
			indexes[startIndex+98] = 2;

			indexes[startIndex+99] = 0;
			indexes[startIndex+100] = 2;
			indexes[startIndex+101] = 3;

			indexes[startIndex+102] = 0;
			indexes[startIndex+103] = 3;
			indexes[startIndex+104] = 4;

			indexes[startIndex+105] = 0;
			indexes[startIndex+106] = 4;
			indexes[startIndex+107] = 5;

			indexes[startIndex + 108] = 0;
			indexes[startIndex + 109] = 5;
			indexes[startIndex + 110] = 6;

			indexes[startIndex + 111] = 0;
			indexes[startIndex + 112] = 6;
			indexes[startIndex + 113] = 7;

			indexes[startIndex + 114] = 0;
			indexes[startIndex + 115] = 7;
			indexes[startIndex + 116] = 8;

			indexes[startIndex + 117] = 0;
			indexes[startIndex + 118] = 8;
			indexes[startIndex + 119] = 1;

			indexes[startIndex + 120] = 9;
			indexes[startIndex + 121] = 11;
			indexes[startIndex + 122] = 10;

			indexes[startIndex + 123] = 9;
			indexes[startIndex + 124] = 12;
			indexes[startIndex + 125] = 11;

			indexes[startIndex + 126] = 9;
			indexes[startIndex + 127] = 13;
			indexes[startIndex + 128] = 12;

			indexes[startIndex + 129] = 9;
			indexes[startIndex + 130] = 14;
			indexes[startIndex + 131] = 13;

			indexes[startIndex + 132] = 9;
			indexes[startIndex + 133] = 15;
			indexes[startIndex + 134] = 14;

			indexes[startIndex + 135] = 9;
			indexes[startIndex + 136] = 16;
			indexes[startIndex + 137] = 15;

			indexes[startIndex + 138] = 9;
			indexes[startIndex + 139] = 17;
			indexes[startIndex + 140] = 16;

			indexes[startIndex + 141] = 9;
			indexes[startIndex + 142] = 10;
			indexes[startIndex + 143] = 17;

			for (int i = startIndex + 96; i < startIndex + 144; i++)
				indexes[i] += 18;
		}
		private void HiltIndexes(int startIndex) {

			for (int c = 0; c < 7; c++) {
				indexes[startIndex + 0 + 48 * c] = 36 + 8 * c;
				indexes[startIndex + 1 + 48 * c] = 45 + 8 * c;
				indexes[startIndex + 2 + 48 * c] = 44 + 8 * c;

				indexes[startIndex + 3 + 48 * c] = 45 + 8 * c;
				indexes[startIndex + 4 + 48 * c] = 36 + 8 * c;
				indexes[startIndex + 5 + 48 * c] = 37 + 8 * c;

				indexes[startIndex + 6 + 48 * c] = 37 + 8 * c;
				indexes[startIndex + 7 + 48 * c] = 46 + 8 * c;
				indexes[startIndex + 8 + 48 * c] = 45 + 8 * c;

				indexes[startIndex + 9 + 48 * c] = 46 + 8 * c;
				indexes[startIndex + 10 + 48 * c] = 37 + 8 * c;
				indexes[startIndex + 11 + 48 * c] = 38 + 8 * c;

				indexes[startIndex + 12 + 48 * c] = 38 + 8 * c;
				indexes[startIndex + 13 + 48 * c] = 47 + 8 * c;
				indexes[startIndex + 14 + 48 * c] = 46 + 8 * c;

				indexes[startIndex + 15 + 48 * c] = 47 + 8 * c;
				indexes[startIndex + 16 + 48 * c] = 38 + 8 * c;
				indexes[startIndex + 17 + 48 * c] = 39 + 8 * c;

				indexes[startIndex + 18 + 48 * c] = 39 + 8 * c;
				indexes[startIndex + 19 + 48 * c] = 48 + 8 * c;
				indexes[startIndex + 20 + 48 * c] = 47 + 8 * c;

				indexes[startIndex + 21 + 48 * c] = 48 + 8 * c;
				indexes[startIndex + 22 + 48 * c] = 39 + 8 * c;
				indexes[startIndex + 23 + 48 * c] = 40 + 8 * c;

				indexes[startIndex + 24 + 48 * c] = 40 + 8 * c;
				indexes[startIndex + 25 + 48 * c] = 49 + 8 * c;
				indexes[startIndex + 26 + 48 * c] = 48 + 8 * c;

				indexes[startIndex + 27 + 48 * c] = 49 + 8 * c;
				indexes[startIndex + 28 + 48 * c] = 40 + 8 * c;
				indexes[startIndex + 29 + 48 * c] = 41 + 8 * c;

				indexes[startIndex + 30 + 48 * c] = 41 + 8 * c;
				indexes[startIndex + 31 + 48 * c] = 50 + 8 * c;
				indexes[startIndex + 32 + 48 * c] = 49 + 8 * c;

				indexes[startIndex + 33 + 48 * c] = 50 + 8 * c;
				indexes[startIndex + 34 + 48 * c] = 41 + 8 * c;
				indexes[startIndex + 35 + 48 * c] = 42 + 8 * c;

				indexes[startIndex + 36 + 48 * c] = 42 + 8 * c;
				indexes[startIndex + 37 + 48 * c] = 51 + 8 * c;
				indexes[startIndex + 38 + 48 * c] = 50 + 8 * c;

				indexes[startIndex + 39 + 48 * c] = 51 + 8 * c;
				indexes[startIndex + 40 + 48 * c] = 42 + 8 * c;
				indexes[startIndex + 41 + 48 * c] = 43 + 8 * c;

				indexes[startIndex + 42 + 48 * c] = 43 + 8 * c;
				indexes[startIndex + 43 + 48 * c] = 36 + 8 * c;
				indexes[startIndex + 44 + 48 * c] = 51 + 8 * c;

				indexes[startIndex + 45 + 48 * c] = 36 + 8 * c;
				indexes[startIndex + 46 + 48 * c] = 44 + 8 * c;
				indexes[startIndex + 47 + 48 * c] = 51 + 8 * c;
			}
		}
		private void BladeIndexes(int startIndex, int startVertex) {
			for (int c = 0; c < 40; c++) {
				indexes[startIndex + 0 + 48 * c] = startVertex + 0 + 8 * c;
				indexes[startIndex + 1 + 48 * c] = startVertex + 9 + 8 * c;
				indexes[startIndex + 2 + 48 * c] = startVertex + 8 + 8 * c;

				indexes[startIndex + 3 + 48 * c] = startVertex + 9 + 8 * c;
				indexes[startIndex + 4 + 48 * c] = startVertex + 0 + 8 * c;
				indexes[startIndex + 5 + 48 * c] = startVertex + 1 + 8 * c;

				indexes[startIndex + 6 + 48 * c] = startVertex + 1 + 8 * c;
				indexes[startIndex + 7 + 48 * c] = startVertex + 10 + 8 * c;
				indexes[startIndex + 8 + 48 * c] = startVertex + 9 + 8 * c;

				indexes[startIndex + 9 + 48 * c] = startVertex + 10 + 8 * c;
				indexes[startIndex + 10 + 48 * c] = startVertex + 1 + 8 * c;
				indexes[startIndex + 11 + 48 * c] = startVertex + 2 + 8 * c;

				indexes[startIndex + 12 + 48 * c] = startVertex + 2 + 8 * c;
				indexes[startIndex + 13 + 48 * c] = startVertex + 11 + 8 * c;
				indexes[startIndex + 14 + 48 * c] = startVertex + 10 + 8 * c;

				indexes[startIndex + 15 + 48 * c] = startVertex + 11 + 8 * c;
				indexes[startIndex + 16 + 48 * c] = startVertex + 2 + 8 * c;
				indexes[startIndex + 17 + 48 * c] = startVertex + 3 + 8 * c;

				indexes[startIndex + 18 + 48 * c] = startVertex + 3 + 8 * c;
				indexes[startIndex + 19 + 48 * c] = startVertex + 12 + 8 * c;
				indexes[startIndex + 20 + 48 * c] = startVertex + 11 + 8 * c;

				indexes[startIndex + 21 + 48 * c] = startVertex + 12 + 8 * c;
				indexes[startIndex + 22 + 48 * c] = startVertex + 3 + 8 * c;
				indexes[startIndex + 23 + 48 * c] = startVertex + 4 + 8 * c;

				indexes[startIndex + 24 + 48 * c] = startVertex + 4 + 8 * c;
				indexes[startIndex + 25 + 48 * c] = startVertex + 13 + 8 * c;
				indexes[startIndex + 26 + 48 * c] = startVertex + 12 + 8 * c;

				indexes[startIndex + 27 + 48 * c] = startVertex + 13 + 8 * c;
				indexes[startIndex + 28 + 48 * c] = startVertex + 4 + 8 * c;
				indexes[startIndex + 29 + 48 * c] = startVertex + 5 + 8 * c;

				indexes[startIndex + 30 + 48 * c] = startVertex + 5 + 8 * c;
				indexes[startIndex + 31 + 48 * c] = startVertex + 14 + 8 * c;
				indexes[startIndex + 32 + 48 * c] = startVertex + 13 + 8 * c;

				indexes[startIndex + 33 + 48 * c] = startVertex + 14 + 8 * c;
				indexes[startIndex + 34 + 48 * c] = startVertex + 5 + 8 * c;
				indexes[startIndex + 35 + 48 * c] = startVertex + 6 + 8 * c;

				indexes[startIndex + 36 + 48 * c] = startVertex + 6 + 8 * c;
				indexes[startIndex + 37 + 48 * c] = startVertex + 15 + 8 * c;
				indexes[startIndex + 38 + 48 * c] = startVertex + 14 + 8 * c;

				indexes[startIndex + 39 + 48 * c] = startVertex + 15 + 8 * c;
				indexes[startIndex + 40 + 48 * c] = startVertex + 6 + 8 * c;
				indexes[startIndex + 41 + 48 * c] = startVertex + 7 + 8 * c;

				indexes[startIndex + 42 + 48 * c] = startVertex + 7 + 8 * c;
				indexes[startIndex + 43 + 48 * c] = startVertex + 0 + 8 * c;
				indexes[startIndex + 44 + 48 * c] = startVertex + 15 + 8 * c;

				indexes[startIndex + 45 + 48 * c] = startVertex + 0 + 8 * c;
				indexes[startIndex + 46 + 48 * c] = startVertex + 8 + 8 * c;
				indexes[startIndex + 47 + 48 * c] = startVertex + 15 + 8 * c;
			}
		}
		public void Indexes() {
			indexes = new int[9000];

			LowestPartIndexes(0);
			//Highest index index is now 144
			HiltIndexes(144);
			//Highest index is now 480
			BladeIndexes(480, 104);
			//Highest index is now 2400
			
			ib = new Direct3D.IndexBuffer(typeof(int), indexes.Length, DXDevice, Direct3D.Usage.WriteOnly, Direct3D.Pool.Default);
			ib.SetData(indexes, 0, Direct3D.LockFlags.None);
		}
		public bool IniDirect3D() {
			try {
				Direct3D.PresentParameters pps = new Direct3D.PresentParameters();
				pps.Windowed = true;
				pps.SwapEffect = Direct3D.SwapEffect.Discard;
				DXDevice = new Direct3D.Device(0, Direct3D.DeviceType.Hardware, this, Direct3D.CreateFlags.SoftwareVertexProcessing, pps);
				DXDevice.RenderState.Lighting = false;
				if (wireFrame)
					DXDevice.RenderState.FillMode = Direct3D.FillMode.WireFrame;
				return true;
			}
			catch (DirectXException dxe) {
				MessageBox.Show(dxe.ToString());
				return false;
			}
		}
	}
}