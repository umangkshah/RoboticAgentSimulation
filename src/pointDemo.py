import cv2

colImg = cv2.imread('../imageData/depth.png')
print(colImg[794][666])
print(colImg[666][794])
cv2.circle(colImg,(390, 1080-323), 2, (0,0,255), 8)
#cv2.circle(colImg,(472, 1080-955), 2, (0,255,255), 8)
cv2.imshow("window",colImg)
cv2.waitKey(0)