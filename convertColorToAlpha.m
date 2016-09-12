[cdata,map,alpha] = imread('roundedButton.png');
% alpha(:,:) = cdata(:,:,1) - (255-alpha(:,:));
% alpha(:,:) = 255-alpha(:,:);

% alpha(:,:) = 255-cdata(:,:,1);
d = size(cdata);
for x = 1 : d(1)
    for y = 1 : d(2)
        if(cdata(x,y,1) ~= 0)
            alpha(x,y) = cdata(x,y,1);
            alpha(x,y) = 255 - alpha(x,y);
        else
            alpha(x,y) = 0;
        end
    end
end

cdata(:,:,:) = 0;



% start = 248;
% stop = 1350;
% 
% gradmult = 0.1;
% gradcurbase = 0.1;
% 
% 
% for i = start:stop  
%     for j = start:stop
%         for k = start:stop
%             alpha(i,j) = gradmult * returncircleparam(alpha(i,j));
%         end
%     end 
% end

imwrite(cdata, 'mainButton.png', 'png', 'alpha', alpha );
%[newcdata,newalpha] = imresize(cdata,map, 0.10);
%imwrite(cdata, 'actionbutton3dalpha160p.png', 'png', 'alpha', newalpha);