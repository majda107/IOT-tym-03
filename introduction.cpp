#include <stdlib.h>
#include <chrono>
#include <thread>
#include <stdio.h>
#include <iostream>

void sleep(int delay) 
{
    std::this_thread::sleep_for(std::chrono::milliseconds(delay));
}

void print_delayed(const char* text, int delay)
{
    auto it = 0;
    while(text[it] != '\0')
    {
        std::cout << text[it] << std::flush;
        sleep(delay);
        it += 1;
    }
}



int main()
{
    // std::cout << "\033[31m" << "lol test" << "\033[0m" << std::endl;

    const char* novak = R"(  _____                           _   _                 _    
 |_   _|__  _ __ ___   __ _ ___  | \ | | _____   ____ _| | __
   | |/ _ \| '_ ` _ \ / _` / __| |  \| |/ _ \ \ / / _` | |/ /
   | | (_) | | | | | | (_| \__ \ | |\  | (_) \ V / (_| |   < 
   |_|\___/|_| |_| |_|\__,_|___/ |_| \_|\___/ \_/ \__,_|_|\_\
                                                             )";

    const char* majda = R"(  __  __            _               _____           _             
 |  \/  | __ _ _ __(_) __ _ _ __   |_   _| __ _ __ | | _____  ___ 
 | |\/| |/ _` | '__| |/ _` | '_ \    | || '__| '_ \| |/ / _ \/ __|
 | |  | | (_| | |  | | (_| | | | |   | || |  | |_) |   < (_) \__ \
 |_|  |_|\__,_|_|  |_|\__,_|_| |_|   |_||_|  | .__/|_|\_\___/|___/
                                             |_|                  )";

    const char* valek = R"(  _          _              __     __    _      _    
 | |   _   _| | ____ _ ___  \ \   / /_ _| | ___| | __
 | |  | | | | |/ / _` / __|  \ \ / / _` | |/ _ \ |/ /
 | |__| |_| |   < (_| \__ \   \ V / (_| | |  __/   < 
 |_____\__,_|_|\_\__,_|___/    \_/ \__,_|_|\___|_|\_\
                                                     )";\


    const char* logo = R"(                                                                               
                                                                               
                                                                               
                                              & &                              
                                              &   &                            
                                              &    &                           
                                              &      &                         
                                              &      &                         
                                              &      %                         
                            &                 &      %                         
                          &     &             &      %                         
                       &&         &           &      %                         
                                    %&        &      %                         
                       &              &&      &                                
                &        &               &&   &                                
             &&&&&        &&                &&&                  &&&&&         
           &&&&&&&          &&                &                       &        
        & &&&&&&&&&&            %              &&&&&&&&&&&&&&&&&&&&&&&&&       
       &&&&&&&&&&&&&              &&                                           
        &&&&&&&&&&&&&& &              &                                        
          &&&&&&&&&&&&&&& &&           &                                       
           &&&&&&&&&&&&&&&&&&&&        &&&&&&&&&&&&&&&                         
            &&&&&&&&&&&&&&&&&&& &                                              
              &&&&&&&&&&&&&&&&&&&                                              
                & &&&&&&&&&&&&&&&&&                                            
                     &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&                    
                      &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&                  
                        &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&                
                         &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&              
                          &                                                    
                                                                               
                                                                               )";




    print_delayed("apt-get install \"navrat-iot\"\n\0", 80);

    print_delayed("\033[33merror: you cannot perform this operation unless you are root.\033[0m\n\n\0", 25);

    sleep(700);
    print_delayed("sudo apt-get install \"navrat-iot\"\n\0", 70);
    print_delayed("\033[1m\033[32mDo you want to install navrat-iot and it's dependencies? [Y/N]: \033[0m\0", 25);
    
    sleep(500);
    print_delayed("Y\n\0", 50);

    print_delayed("\033[1m\033[32m\nInstalling navrat-iot (3 dependencies) https://kyberna.cz/\0", 25);
    print_delayed("......\033[0m\n\n\0", 340);

    sleep(1400);

    print_delayed("\033[1m\033[33m\0", 0);
    print_delayed(novak, 2);
    print_delayed("\033[0m\n\n\0", 0);
    sleep(300);
    print_delayed("Age: 19\n\0", 40);
    print_delayed("Study: Networking\n\0", 40);
    print_delayed("Interests: Mikrotik, CISCO\n\n\0", 40);

    sleep(1400);

    print_delayed("\033[1m\033[33m\0", 0);
    print_delayed(majda, 2);
    print_delayed("\033[0m\n\n\0", 0);
    sleep(300);
    print_delayed("Age: 16\n\0", 40);
    print_delayed("Study: Programming\n\0", 40);
    print_delayed("Interests: OpenGL (Computer Graphics), C++, C#\n\n\0", 40);

    sleep(1400);

    print_delayed("\033[1m\033[33m\0", 0);
    print_delayed(valek, 2);
    print_delayed("\033[0m\n\n\0", 0);
    sleep(300);
    print_delayed("Age: 19\n\0", 40);
    print_delayed("Study: Programming\n\0", 40);
    print_delayed("Interests: Gaming, 12-\n\n\0", 40);

    sleep(500);

    print_delayed("\033[1m\033[33m\0", 0);
    print_delayed(logo, 1);
    print_delayed("\033[0m\n\n\0", 0);

    print_delayed("Střední škola a vyšší odborná škola aplikované kybernetiky s.r.o.\n\0", 40);
    print_delayed("https://kyberna.cz/\n\n\0", 40);

    print_delayed("Team: 03\n\0", 40);
    print_delayed("Name: Navrat\n\0", 40);
    print_delayed("Tasks: 1-12 done\n\n\0", 40);

    print_delayed("Have a nice day!\n\0", 40);

    print_delayed("\033[1m\033[32m\nInstalled!\n\n\0", 25);

    return 0;
}
