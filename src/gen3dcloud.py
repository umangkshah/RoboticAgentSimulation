import cv2
import numpy as np


class Point3d:
  
  def __init__(self, rgb, x, y, z):
    self.rgb = rgb
    self.x = x
    self.y = y
    self.z = z  
  
  def __str__(self):
    txt = str(self.x)+","+str(self.y)+","+str(self.z)+","+str(self.rgb[0])+","+str(self.rgb[1])+","+str(self.rgb[2])+"\n"
    return txt
    
  def write_file(p3list, fname):
    fout = open(fname,'w')
    for p3d in p3list:
      fout.write(str(p3d))
    fout.close()
      
    

def project(cloud):
  image = np.zeros((height,width,3), np.uint8)
  
    
def main():
  colImg = cv2.imread('../imageData/color.jpg')
  depthImg = cv2.imread('../imageData/depth.png',0)
  
  print(colImg.shape)
  
  fxy = [1036.1, 1038.4]
  cxy = [953.2, 536.71]
  shx = 5.2187
  pointcloud = []
  for i in range(colImg.shape[1]):#width
    for j in range(colImg.shape[0]):#height
      z3d = depthImg[j][i]
      rgb = colImg[j][i]
      if z3d > 0:
        x2d = i
        y2d = colImg.shape[0] - j
        y3d = ((y2d - cxy[1])/fxy[1]) * z3d;
        shx2d = (y3d/z3d)*shx
        x3d = ((x2d - cxy[0] - shx2d)/fxy[0]) * z3d
        pointcloud.append(Point3d(rgb,x3d,y3d,z3d))
#      print("color : ", colImg[i][j])#the rgb value
   #project(pointcloud)
  Point3d.write_file(pointcloud,"./pcloud.csv")

main()
'''
p3list = []
p3list.append(Point3d([1,1,1],5,6,7))
p3list.append(Point3d([1,1,1],5,6,7))
p3list.append(Point3d([1,1,1],5,6,7))
Point3d.write_file(p3list,"./pcloud.csv")
'''
