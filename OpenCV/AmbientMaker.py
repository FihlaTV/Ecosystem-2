import sys
import numpy as np
import cv2 as cv

#Declaração de variáveis globais e argumentos
if(len(sys.argv) < 2):
    print("O programa requer uma imagem de entrada.")
    exit(0)

filename = sys.argv[1]
img = cv.imread(filename)

#Se o usuário especificou um nome para a saída, ele será salvo. Senão, será o nome original acrescido de "binary"
if(len(sys.argv) == 3):
    savename = sys.argv[2]
else:
    savename = filename.replace(".png", "binary.png")

#Define Tamanho da imagem
width = img.shape[1]
height = img.shape[0]

#Função de evento de mouse
def erase_color(event,x,y,flags,param):

    global img

    #Botão Direito -- Binarizar a imagem.
    if event == cv.EVENT_RBUTTONDOWN:
        
        gray = cv.cvtColor(img,cv.COLOR_BGR2GRAY)
        img = gray

    #Botão Esquerdo -- Transforma a cor escolhida em branco por toda a imagem.
    elif event == cv.EVENT_LBUTTONDOWN:
        
        blue = img.item(y,x,0)
        green = img.item(y,x,1)
        red = img.item(y,x,2)

        for i in range(width):
            for j in range(height):
                if img.item(j,i,0) == blue and img.item(j,i,1) == green and img.item(j,i,2) == red:
                    img[j,i] = [255,255,255]
    

    #Roda do Mouse
    elif event == cv.EVENT_MOUSEWHEEL:

        #Girar para cima: Dilata o branco da imagem
        if flags > 0:
            ret,thresh1 = cv.threshold(img,250,255,cv.THRESH_BINARY)

            kernel = np.ones((3,3),np.uint8)
            dst = cv.dilate(thresh1,kernel,iterations = 1)
            img = dst

        #Girar para baixo: Erode o branco da imagem
        else:
            ret,thresh1 = cv.threshold(img,250,255,cv.THRESH_BINARY)

            kernel = np.ones((3,3),np.uint8)
            dst = cv.erode(thresh1,kernel,iterations = 1)
            img = dst



#MAIN
cv.namedWindow(filename)
cv.setMouseCallback(filename,erase_color)

while(1):
    cv.imshow(filename,img)
    k = cv.waitKey(33) 
    #ESC -- Sai do programa
    if  k == 27:
        break
    #Barra de Espaço -- Salva a imagem
    elif k == 32:
        cv.imwrite(savename, img  )
cv.destroyAllWindows()